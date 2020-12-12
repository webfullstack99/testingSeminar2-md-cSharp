using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralTest.PageMethods;
using NUnit.Framework;

namespace GeneralTest.TestCases
{
    class Sendo_script_3 : SenDoTest
    {
        // SCRIPT THREE
        [Test]
        [Category("ScriptThree")]
        public void scriptThree_testOne()
        {
            // show info
            sendoPage = new SenDoPage(getDriver());
            sendoPage.navigateToHomePage();
            sendoPage.closeAdPopup();
        }

    }
}
