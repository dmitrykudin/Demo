using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDatabase.EntityModels;

namespace EntityDatabase.DataLayer.MSSQL
{
    public class TagRepository : ITagRepository
    {
        public Tag CreateTag(string tagName)
        {
            using (var cc = new CustomersContext())
            {
                Tag tag = cc.Tags.FirstOrDefault(t => t.TagName == tagName);
                if (tag == null)
                {
                    tag = cc.Tags.Add(new Tag()
                    {
                        TagName = tagName
                    });
                    cc.SaveChanges();
                }
                return tag;
            }

        }

        public List<Tag> GetAllTags()
        {
            using (var cc = new CustomersContext())
            {
                return cc.Tags.ToList();
            }
        }
    }
}
