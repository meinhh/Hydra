using System;
using Hydra.DAL;
using Hydra.Data;

namespace Hydra.BL
{
    public class CommentBl
    {
        private readonly ProductDataAccess _productDal;

        public CommentBl(HydraContext context)
        {
            _productDal = new ProductDataAccess(context);
        }

        public void Add(string productId, string text)
        {
            var comment = new Models.Comment
            {
                Text = text,
                Date = DateTime.Now,
                Publisher = new Models.User { Name = "The Doctor" }
            };

            _productDal.AddComment(int.Parse(productId), comment);
        }
    }
}
