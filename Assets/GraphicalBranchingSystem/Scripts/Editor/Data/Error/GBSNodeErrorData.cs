using System.Collections.Generic;

namespace GBS.Data.Error
{
    using Elements;

    public class GBSNodeErrorData
    {
        public GBSErrorData ErrorData { get; set; }
        public List<GBSNode> Nodes { get; set; }

        public GBSNodeErrorData() 
        {
            ErrorData = new GBSErrorData();
            Nodes = new List<GBSNode>();
        }
    }
}
