using AutoMapper;
using FCGagarin.Domain.Model;
using FCGagarin.WebUI.ViewModels;

namespace FCGagarin.WebUI.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelToDomainMappings";
            }
        }

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<NewsViewModel, News>().ForMember(x => x.Author, opt => opt.Ignore());
            CreateMap<NewsFormModel, News>();//.ForMember(x => x.AuthorId, opt => opt.MapFrom(source=>source.AuthorId));
        }
    }
}