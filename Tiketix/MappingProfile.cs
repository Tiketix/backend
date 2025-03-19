

using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>()
                .ForMember(c => c.Email,
                    opt => opt.MapFrom(c => c.Email));

        CreateMap<Event, EventDto>()
                .ForMember(c => c.OrganizerEmail,
                    opt => opt.MapFrom(c => c.OrganizerEmail));

        CreateMap<User, LoginDto>()
                .ForMember(c => c.Email,
                    opt => opt.MapFrom(c => c.Email));

        CreateMap<AddClientDto, Client>();

        CreateMap<UpdateClientNameDto, Client>();

        CreateMap<UpdateClientContactDto, Client>();
        
        CreateMap<UpdatePasswordDto, Client>();

        CreateMap<AddEventDto, Event>();

        CreateMap<RegistrationDto, User>();


    }
}
