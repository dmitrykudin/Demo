using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase.DataLayer
{
    public interface ITagRepository
    {
        Tag CreateTag(string tagName);
        List<Tag> GetAllTags();
    }
}
