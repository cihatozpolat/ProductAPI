using System;
using System.Collections.Generic;
using domain.NHibernateHelper;
using domain.Repository;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using domain;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected ISessionFactory sessionFactory;
        public ProductController()
        {
            sessionFactory = NHibernateHelper.CreateSessionFactory();
        }

        // GET api/product/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductLessInfo> GetById([FromRoute]int id)
        {

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var productRepository = new Repository<Product>(session);
                    var target = productRepository.GetById(id);
                    var less = new ProductLessInfo() { Id = target.Id, Name = target.Name, Color = target.Color };
                    return less;
                }
            }
        }

        // GET api/product/{id}/detail
        [HttpGet("{id}/detail")]
        public ActionResult<Product> GetWithDetail([FromRoute]int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var productRepository = new Repository<Product>(session);
                    return productRepository.GetById(id);
                }
            }
        }

        // PUT api/product/{request}
        [HttpPut()]
        public void Put([FromBody] ProductRequest request)
        {
            var d1 = new DateTime(0001, 01, 01, 00, 00, 00);
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var productRepository = new Repository<Product>(session);
                    var p1 = productRepository.GetById(request.Id);

                    if (!string.IsNullOrEmpty(request.Name)) { p1.Name = request.Name; }
                    if (!string.IsNullOrEmpty(request.Color)) { p1.Color = request.Color; }
                    if (!(request.Price == 0)) { p1.Price = request.Price; }
                    if (!(request.ExpirationDate == d1)) { p1.ExpirationDate = request.ExpirationDate; }
                    if (!string.IsNullOrEmpty(request.Country)) { p1.Country = request.Country; }

                    productRepository.Save(p1);
                    transaction.Commit();
                }
            }
        }

        // POST api/product/{request}
        [HttpPost()]
        public void Post([FromBody] List<ProductRequest> request)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var productRepository = new Repository<Product>(session);

                    foreach (var x in request)
                    {
                        var p1 = new Product
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Color = x.Color,
                            Price = x.Price,
                            ExpirationDate = x.ExpirationDate,
                            Country = x.Country
                        };

                        productRepository.Save(p1);
                    }

                    transaction.Commit();
                }
            }
        }

        // DELETE api/product/5
        [HttpDelete()]
        public void Delete([FromQuery]List<int> Ids)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var productRepository = new Repository<Product>(session);

                    for (int i = 0; i < Ids.Count; i++)
                    {
                        var target = productRepository.GetById(Ids[i]);
                        productRepository.Delete(target);
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
