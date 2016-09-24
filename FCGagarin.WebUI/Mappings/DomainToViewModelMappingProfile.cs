using AutoMapper;
using FCGagarin.Domain.Model;
using FCGagarin.WebUI.ViewModels;

namespace FCGagarin.WebUI.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<News, NewsViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(source => source.Author.ToString()));
            CreateMap<News, NewsFormModel>();
        }
    }
}