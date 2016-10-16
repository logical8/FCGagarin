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

            CreateMap<VideoAlbum, VideoAlbumViewModel>();
            CreateMap<VideoAlbum, VideoAlbumFormModel>();
            CreateMap<VideoAlbum, VideoAlbumDetailsViewModel>()
                .ForMember(vadbm => vadbm.VideoAlbumViewModel, opt => opt.MapFrom(source => source))
                .ForMember(vadbm => vadbm.VideoViewModelList, opt => opt.MapFrom(source => source.Videos));

            CreateMap<Video, VideoViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(source => source.Author.ToString()));
            CreateMap<Video, VideoFormModel>();
        }
    }
}