using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_LampshadeQuery.Contracts.Slide;
using ShopManagement.Infrastucture.EfCore;

namespace _01_LampshadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides
                .Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel
                {
                    Picture = x.Picture,
                    Heading = x.Heading,
                    BtnText = x.BtnText,
                    Link = x.Link,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Text = x.Text,
                    Title = x.Title

                }).OrderByDescending(x => x.Picture).ToList();
        }
    }
}
