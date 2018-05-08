﻿using Payroll.DataModel;
using Payroll.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Repository
{
    public class SellingDetailRepo
    {
        public static List<SellingDetailViewModel> Get()
        {
            List<SellingDetailViewModel> result = new List<SellingDetailViewModel>();
            using (var db = new PayrollContext())
            {
                result = (from d in db.SellingDetail
                          select new SellingDetailViewModel
                          {
                              Id = d.Id,
                              SellingHeaderId = d.SellingHeaderId,
                              ItemId = d.ItemId,
                              Quantity = d.Quantity,
                              Price = d.Price,
                              Amount = d.Amount,
                              IsActivated = d.IsActivated
                          }).ToList();
            }
            return result;
        }
        public static SellingDetailViewModel GetById(int id)
        {
            SellingDetailViewModel result = new SellingDetailViewModel();
            using (var db = new PayrollContext())
            {
                result = (from d in db.SellingDetail
                          where d.Id == id
                          select new SellingDetailViewModel
                          {
                              Id = d.Id,
                              SellingHeaderId = d.SellingHeaderId,
                              ItemId = d.ItemId,
                              Quantity = d.Quantity,
                              Price = d.Price,
                              Amount = d.Amount,
                              IsActivated = d.IsActivated
                          }).FirstOrDefault();
            }
            return result;
        }

        public static bool Update(SellingDetailViewModel entity)
        {
            bool result = true;
            try
            {
                using (var db = new PayrollContext())
                {
                    if (entity.Id != 0)
                    {
                        SellingDetail sd = db.SellingDetail.Where(o => o.Id == entity.Id).FirstOrDefault();
                        if (sd != null)
                        {
                            sd.SellingHeaderId = entity.SellingHeaderId;
                            sd.ItemId = entity.ItemId;
                            sd.Quantity = entity.Quantity;
                            sd.Price = entity.Price;
                            sd.Amount = entity.Amount;
                            sd.IsActivated = entity.IsActivated;
                            sd.ModifyBy = "Azam";
                            sd.ModifyDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        SellingDetail sd = new SellingDetail();
                        sd.SellingHeaderId = entity.SellingHeaderId;
                        sd.ItemId = entity.ItemId;
                        sd.Quantity = entity.Quantity;
                        sd.Price = entity.Price;
                        sd.Amount = entity.Amount;
                        sd.IsActivated = entity.IsActivated;
                        sd.CreateBy = "Azam";
                        sd.CreateDate = DateTime.Now;
                        db.SellingDetail.Add(sd);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static bool Delete(int id)
        {
            bool result = true;
            try
            {
                using (var db = new PayrollContext())
                {
                    SellingDetail sd = db.SellingDetail.Where(o => o.Id == id).FirstOrDefault();
                    if (sd != null)
                    {
                        db.SellingDetail.Remove(sd);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}