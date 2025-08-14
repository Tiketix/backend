
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class TicketService : ITicketService
{
    private readonly IRepositoryManager _repository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;


    public TicketService(IRepositoryManager repository, IMapper mapper, UserManager<User> userManager)
    {
        _repository = repository;
        _mapper = mapper;
        _userManager = userManager;
    }


    public async Task<ApiResponse<UserTicketsDto>> GetAllUserTickets(GetTicketRequest request, bool trackChanges)
    {
        var user =  await _userManager.FindByIdAsync(request.Id);
        if (user == null)
            return ApiResponse<UserTicketsDto>.FailureResponse(new List<string> { "User does not exist in Database" });

        var tickets = await _repository.Ticket.GetAllUserTickets(request.Id, trackChanges);
        if (tickets == null || !tickets.Any())
            return ApiResponse<UserTicketsDto>.FailureResponse(new List<string> { "No tickets found for this user." });

        // Group and project ticket count by event with name
        var ticketCountByEvent = tickets
            .GroupBy(t => new { t.EventId, t.EventDetails.EventTitle }) // possible cos navigation property exists
            .Select(g => new EventTicketCountDto
            {
                EventId = g.Key.EventId,
                EventName = g.Key.EventTitle,
                TicketCount = g.Count()
            })
            .ToList();

        var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);

        var response = new UserTicketsDto
        {
            Tickets = ticketsDto,
            TicketCountByEvent = ticketCountByEvent
        };
        return ApiResponse<UserTicketsDto>.SuccessResponse(response, "All User Tickets retrieved successfully");
    }

    public async Task<ApiResponse<IEnumerable<TicketDto>>> AddTicket(AddTicketDto newTicket)
    {
        if (newTicket == null)
            return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { "New ticket data is null" });

        if (string.IsNullOrEmpty(newTicket.UserId))
            return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { "User ID is required" });

        if (newTicket.EventId == Guid.Empty)
            return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { "Event ID is required" });

        if (newTicket.NoOfTickets <= 0)
            return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { $"Invalid number of tickets ordered" });

        using var transaction = await _repository.BeginTransactionAsync();

        try
        {
            //get Event to check if it exists and if it has available tickets
            var eventDetails = await _repository.Event.GetEventById(newTicket.EventId, trackChanges: false);
            if (eventDetails == null)
                return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { "Event does not exist" });

            if (eventDetails.TicketsAvailable <= 0)
                return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { "No tickets available for this event" });

            if (newTicket.NoOfTickets > eventDetails.TicketsAvailable)
                return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { $"We have only {eventDetails.TicketsAvailable} ticket(s) left" });

            // Create list to store all created tickets
            var createdTickets = new List<Ticket>();
            var purchaseTime = DateTime.UtcNow;
            // create number of tickets based on NoOfTickets ordered
            for (int i = 0; i < newTicket.NoOfTickets; i++)
            {
                var ticketDto = new AddTicketDto
                {
                    UserId = newTicket.UserId,
                    EventId = newTicket.EventId,
                    NoOfTickets = 1, // Each ticket is created individually
                    PurchaseTime = purchaseTime,
                };
                var ticket = _mapper.Map<Ticket>(ticketDto);
                createdTickets.Add(ticket);
            }

            // Batch add all tickets.
            await _repository.Ticket.AddRange(createdTickets);
            
            // Update the event's available tickets
            eventDetails.TicketsAvailable -= newTicket.NoOfTickets; // Decrease available tickets
            await _repository.Event.UpdateEvent(eventDetails); // Update the event in the repository

            await _repository.Save();
            await transaction.CommitAsync();
            var returnTickets = _mapper.Map<List<TicketDto>>(createdTickets);
            return ApiResponse<IEnumerable<TicketDto>>.SuccessResponse(returnTickets, "Tickets added successfully");
        }
        catch (Exception ex)
        {
            // Rollback transaction on error
            await transaction.RollbackAsync();
            
            return ApiResponse<IEnumerable<TicketDto>>.FailureResponse(new List<string> { "An error occurred while processing your ticket order", ex.Message });
        }
        
    }
}
