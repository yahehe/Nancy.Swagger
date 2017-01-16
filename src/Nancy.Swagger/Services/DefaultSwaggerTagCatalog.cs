using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public class DefaultSwaggerTagCatalog : List<Tag>, ISwaggerTagCatalog
    {
        public DefaultSwaggerTagCatalog(IEnumerable<ISwaggerTagProvider> tags)
        {
            AddRange(tags.Select(x => x.GetTag()));
        }

        public void AddTag(Tag tag)
        {
            if(this.Count(x => x.Name.Equals(tag.Name)) == 0) Add(tag);
        }
    }
}
