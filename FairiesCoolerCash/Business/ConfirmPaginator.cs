using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
namespace FairiesCoolerCash.Business
{
    public class ConfirmPaginator:DocumentPaginator
    {
        public ConfirmPaginator()
        {
        }
        public override DocumentPage GetPage(int pageNumber)
        {
            throw new NotImplementedException();
        }

        public override bool IsPageCountValid
        {
            get { return true; }
        }

        public override int PageCount
        {
            get { return 1; }
        }

        public override Size PageSize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get { throw new NotImplementedException(); }
        }
    }
}
