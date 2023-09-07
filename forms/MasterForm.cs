using CardPrinterUI.interfaces;
using System.Runtime.CompilerServices;

namespace CardPrinterUI
{
    public partial class masterForm : Form, IForm
    {
        public masterForm(IDataStore datastore)
        {
            GuardNet.Guard.NotNull(datastore, nameof(datastore));

            this.datastore = datastore;

            InitializeComponent();
        }

        private readonly IDataStore datastore;

        public Form GetForm()
        {
            return this;
        }
    }
}