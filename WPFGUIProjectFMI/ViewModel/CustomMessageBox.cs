﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGUIProjectFMI
{
    class CustomMessageBox : BaseViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ButtonText { get; set; }
    }
}
