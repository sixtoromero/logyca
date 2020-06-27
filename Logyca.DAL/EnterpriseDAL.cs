using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Logyca.Models;

namespace Logyca.DAL
{
    public class EnterpriseDAL
    {
        private readonly DbContextOptions<LogycaDbContext> options;
        public EnterpriseDAL(DbContextOptions<LogycaDbContext> options = null)
        {
            this.options = options;
        }

        public string Insert(Enterprise model)
        {
            try
            {
                string message = string.Empty;
                using (var context = new LogycaDbContext(this.options))
                {
                    Enterprise result = new Enterprise();
                    result = context.Enterprises.Where(e => e.Id.Equals(model.Id) || e.Nit.Equals(model.Nit)).FirstOrDefault();
                    if (result == null)
                    {
                        context.Enterprises.Add(model);
                        context.SaveChanges();
                        message = "success";
                    }
                    else
                    {
                        message = "El Nit ya existe";
                    }

                    return message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }

        public string Update(Enterprise model)
        {
            try
            {
                string message = string.Empty;
                using (var context = new LogycaDbContext(this.options))
                {
                    Enterprise result = new Enterprise();
                    result = context.Enterprises.Where(e => e.Id.Equals(model.Id) || e.Nit.Equals(model.Nit)).FirstOrDefault();
                    if (result != null)
                    {
                        result.Name = model.Name;
                        result.Gln = model.Gln;
                        context.Entry(result).State = EntityState.Modified;

                        context.SaveChanges();
                        message = "success";
                    }
                    else
                    {
                        message = "El Nit ya existe";
                    }

                    return message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Enterprise> GetAllEnterprise()
        {
            try
            {
                List<Enterprise> result = new List<Enterprise>();
                using (var context = new LogycaDbContext(this.options))
                {
                    result = context.Enterprises
                                       .Include(c => c.Code)
                                       .OrderBy(c => c.Name).ToList();
                }

                foreach (var item in result)
                {
                    foreach (var code in item.Code)
                    {
                        code.Enterprise = null;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Enterprise> GetEnterpriseByNit(int Nit)
        {
            try
            {
                List<Enterprise> result = new List<Enterprise>();
                using (var context = new LogycaDbContext(this.options))
                {
                    result = context.Enterprises
                                       .Include(c => c.Code)
                                       .Where(e => e.Nit.Equals(Nit))
                                       .OrderBy(c => c.Name).ToList();
                }

                foreach (var item in result)
                {
                    foreach (var code in item.Code)
                    {
                        code.Enterprise = null;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Enterprise GetEnterpriseById(int Id)
        {
            try
            {
                Enterprise result = new Enterprise();
                using (var context = new LogycaDbContext(this.options))
                {
                    result = context.Enterprises
                                       .Include(c => c.Code)
                                       .Where(e => e.Id.Equals(Id)).FirstOrDefault();
                }

                foreach (var code in result.Code)
                {
                    code.Enterprise = null;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Enterprise> GetEnterpriseByIdCode(int IdCode)
        {
            try
            {
                List<Enterprise> result = new List<Enterprise>();
                using (var context = new LogycaDbContext(this.options))
                {
                    result = (from e in context.Enterprises
                              join c in context.Codes
                              on e.Id equals c.Owner
                              where c.Id == IdCode
                              select new Enterprise
                              {
                                  Id = e.Id,
                                  Name = e.Name,
                                  Nit = e.Nit,
                                  Gln = e.Gln
                              }).ToList();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
