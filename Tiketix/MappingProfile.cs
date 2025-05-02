

using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventDto>()
                .ForMember(c => c.OrganizerEmail,
                    opt => opt.MapFrom(c => c.OrganizerEmail));

        CreateMap<Ticket, TicketDto>()
                .ForMember(c => c.Email,
                    opt => opt.MapFrom(c => c.Email));

        CreateMap<User, LoginDto>()
                .ForMember(c => c.Email,
                    opt => opt.MapFrom(c => c.Email));
        CreateMap<EmailVerificationToken, EmailVerificationTokenDto>()
                .ForMember(c => c.Email,
                    opt => opt.MapFrom(c => c.Email));


        CreateMap<AddEventDto, Event>();

        CreateMap<RegistrationDto, User>();

        CreateMap<AdminRegistrationDto, User>();

        CreateMap<UpdateEventDetailsDto, Event>();

        CreateMap<AddEmailVerificationTokenDto, EmailVerificationToken>();

        CreateMap<AddTicketDto, Ticket>();

    }
}
