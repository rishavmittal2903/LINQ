using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CombineData
    {
        public CombineData()
        {
            ItemDetails = new List<PreLimItem>();
        }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }

        public List<PreLimItem> ItemDetails { get; set; }

    }
    public class PreLimItem
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }

        public ItemDetails ItemData { get; set; }

    }
    public class ItemDetails
    {
        public int Cost { get; set; } = 0;

        public int Rate { get; set; } = 0;
        public int GrossMargin { get; set; } = 0;
        public int NoOfHours { get; set; } = 0;

    }
    public class LookUp
    {
        public int LookUpId { get; set; }
        public string LookUpItem { get; set; }
        public string LookUpKey { get; set; }
        public string Description { get; set; }
    }
    public class PreMapping
    {
        public int PreMapId { get; set; }
        public string ComponentId { get; set; }
        public string ItemId { get; set; }
    }
    public class Prelim
    {
        public int PreLimId { get; set; }
        public string ProjectId { get; set; }
    }
    public class PreLimComponents
    {
        public int PreLimId { get; set; }
        public string PreLimComponentId { get; set; }
    }
    public class PreLimItems
    {
        public string PreLimComponentId { get; set; }
        public string ItemId { get; set; }

        public int Cost { get; set; }

        public int Rate { get; set; }
        public int GrossMargin { get; set; }
        public int NoOfHours { get; set; }
    }
    class Program
    {
        static List<LookUp> lstLookUp = new List<LookUp>();
        static List<PreMapping> lstMapping = new List<PreMapping>();
        static List<Prelim> lstPre = new List<Prelim>();
        static List<PreLimComponents> lstPreComp = new List<PreLimComponents>();
        static List<PreLimItems> lstPreItem = new List<PreLimItems>();
        static List<CombineData> lstCombineData = new List<CombineData>();
        static string[] alpha = { "A", "B", "C", "D", "E", "F", "G", "H", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
        static string[] items = { "SUM", "ADD", "SUBTRACT", "DIVIDE", "TEST", "LUMSUM" };
        static void Main(string[] args)
        {
            for (int i = 1; i < 14; i++)
            {
                LookUp lk = new LookUp();
                lk.LookUpId = i;
                lk.LookUpKey = i.ToString();
                lk.LookUpItem = "Pre_Components";
                lk.Description = alpha[i];
                lstLookUp.Add(lk);

            }
            for (int i = 1; i < 5; i++)
            {
                LookUp lk = new LookUp();
                lk.LookUpId = i;
                lk.LookUpKey = i.ToString();
                lk.LookUpItem = "Pre_Component_Items";
                lk.Description = items[i];
                lstLookUp.Add(lk);
            }
            for (int i = 1; i < 14; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    PreMapping lk = new PreMapping();
                    lk.PreMapId = i + j + 11;
                    lk.ItemId = j.ToString();
                    lk.ComponentId = j.ToString();
                    lstMapping.Add(lk);
                }
            }
            Prelim lim = new Prelim() { PreLimId = 1, ProjectId = 11.ToString() };
            lstPre.Add(lim);
            for (int i = 1; i < 14; i++)
            {
                PreLimComponents precomp = new PreLimComponents() { PreLimId = 1, PreLimComponentId = i.ToString() };
                lstPreComp.Add(precomp);

            }
            for (int j = 1; j < 5; j++)
            {
                PreLimItems preItem = new PreLimItems() { PreLimComponentId = 1.ToString(), ItemId = j.ToString(), Rate = 11, GrossMargin = 1 };
                lstPreItem.Add(preItem);
            }
            CombineData cDtaa = new CombineData();
            var data = (from d in lstPre.Where(x => x.ProjectId == "11")
                        join s in lstPreComp on d.PreLimId equals s.PreLimId into p
                        from ss in p.DefaultIfEmpty(new PreLimComponents() { PreLimComponentId=string.Empty})
                        join j in lstPreItem on ss.PreLimComponentId equals j.PreLimComponentId into q
                        from jj in q.DefaultIfEmpty(new PreLimItems()
                        {
                            PreLimComponentId=string.Empty,
                            ItemId = string.Empty,
                            NoOfHours=0,
                            Rate=0,
                            GrossMargin=0,
                        }) 
                        select new
                        {

                            ProjectId= (d != null ? d.ProjectId : ""),
                            PreLimComponentId= (ss != null ? ss.PreLimComponentId : ""),
                            ItemId= (jj!=null?jj.ItemId:""),
                            NoOfHours= (jj != null ? jj.NoOfHours : 0),
                            Rate = (jj != null ? jj.Rate : 0),
                            GrossMargin = (jj != null ? jj.GrossMargin : 0)

                        }).ToList();
            //var g = lstLookUp.Where(x => x.LookUpItem == "Pre_Components" || x.LookUpItem == "Pre_Component_Items").ToList();
            //var lookUpData = (
                              
                              
            //                  from s in lstMapping

            //                  select new {
            //                      ComponentId=s.ComponentId,
            //                      ComponentName=g.Select(x=>x.LookUpItem=="Pre_Components" && x.LookUpKey == s.ComponentId):"",
            //                      ItemId= s.ItemId,
            //                      ItemName
            //                      if(data.)

            //                  }).ToList();
            //lookUpData.ForEach(x =>
            //{
            //    CombineData combineData = new CombineData();
            //    PreLimItem preLimItem = new PreLimItem();  
            //    ItemDetails itemDetails = new ItemDetails();

            //    if (x.LookUpItem == "Pre_Components" && x.LookUpKey == x.ComponentId)
            //    {
            //        combineData.ComponentId = x.ComponentId;
            //        combineData.ComponentName = x.Description;

            //    }
            //    if (x.LookUpItem == "Pre_Component_Items" && x.LookUpKey == x.ComponentId)
            //    {
            //        preLimItem.ItemId = x.ItemId;
            //        preLimItem.ItemName = x.Description;

            //    }
            //    combineData.ItemDetails.Add(preLimItem);
            //    lstCombineData.Add(combineData);
            //});
            Console.ReadLine();
        }
    }
}
