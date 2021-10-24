using System.Collections.Generic;

namespace RestWithASPNETUdemy.Model
{
    public abstract class RestModelBase
    {
        public List<Link> Links { get; set; } = new List<Link>();
    }
}