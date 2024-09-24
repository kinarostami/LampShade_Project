using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastucture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastucture.EfCore;

namespace ShopManagement.Infrastucture.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long,Slide>, ISlideRepository
    {
        private readonly ShopContext _shopContext;
        public SlideRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditSlide GetDetails(long id)
        {
            return _shopContext.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                //Picture = x.Picture,
                BtnText = x.BtnText,
                Heading = x.Heading,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Title = x.Title,
                Link = x.Link

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _shopContext.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Heading = x.Heading,
                Title = x.Title,
                IsRemove = x.IsRemoved,
                CreationDate = x.CreationDate.ToFarsi()

            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
