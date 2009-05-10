using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DirectShowLib;

using Panoply.Library.Presentation;

namespace Panoply.Gui
{
    public partial class SetMeritForm : Form
    {
        Dictionary<RadioButton, Merit> _radioButtonMerits = new Dictionary<RadioButton, Merit>();
        FilterTreeNode _filter;

        [Obsolete("Designer use only", true)]
        public SetMeritForm()
        {
            InitializeComponent();
        }

        public SetMeritForm(FilterTreeNode filter)
        {
            InitializeComponent();

            _filter = filter;

            //Configure the merits for each radio button
            _radioButtonMerits.Add(meritSwCompressorRadioButton, Merit.SWCompressor);
            _radioButtonMerits.Add(meritHwCompressorRadioButton, Merit.HWCompressor);
            _radioButtonMerits.Add(meritDoNotUseRadioButton, Merit.DoNotUse);
            _radioButtonMerits.Add(meritUnlikelyRadioButton, Merit.Unlikely);
            _radioButtonMerits.Add(meritNormalRadioButton, Merit.Normal);
            _radioButtonMerits.Add(meritNormalPlus1RadioButton, Merit.Normal + 1);
            _radioButtonMerits.Add(meritPreferredRadioButton, Merit.Preferred);
            _radioButtonMerits.Add(meritPreferredPlusOneRadioButton, Merit.Preferred + 1);
            _radioButtonMerits.Add(meritPreferredPlus255RadioButton, Merit.Preferred + 255);

            customMeritTextBox.Text = ((uint)_filter.Merit).ToString("x");

            filterNameLabel.Text = _filter.FriendlyName;

            //Check the relevant radio button
            bool matchFound = false;
            foreach (RadioButton key in _radioButtonMerits.Keys)
            {
                if (_radioButtonMerits[key] == _filter.Merit)
                {
                    key.Checked = true;
                    matchFound = true;
                    break;
                }
            }

            if (!matchFound)
            {
                meritCustomRadioButton.Checked = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //Translate the checked radio button into a merit level
            Merit merit = Merit.None;
            bool match = false;
            foreach (RadioButton key in _radioButtonMerits.Keys)
            {
                if (key.Checked)
                {
                    merit = _radioButtonMerits[key];
                    match = true;
                    break;
                }
            }

            if (!match)
            {
                if (!meritCustomRadioButton.Checked)
                {
                    MessageBox.Show(this, "You must check one of the radio buttons");
                    return;
                }

                uint rawMerit;
                if (!uint.TryParse(customMeritTextBox.Text, out rawMerit))
                {
                    MessageBox.Show(this, "The custom merit value is not a valid hexadecimal number");
                    return;
                }

                merit = (Merit)rawMerit;
            }

            if (merit != _filter.Merit)
            {
                try
                {
                    _filter.SetMerit(merit);
                }
                catch (Exception err)
                {
                    MessageBox.Show(this,
                        String.Format("Unable to set merit for filter: {0}", err.Message),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            //Worked
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void meritCustomRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            customMeritTextBox.Enabled = meritCustomRadioButton.Checked;
        }
    }
}
