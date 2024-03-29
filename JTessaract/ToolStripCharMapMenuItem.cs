﻿/*
 * Copyright 2008 Ruwan Janapriya Egoda Gamage. http://www.janapriya.net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JTessaract
{
    public delegate void CharacterMapClickEventDelegate(string charactor);

    public class ToolStripCharMapMenuItem : ToolStripMenuItem
    {
        private string character;

        public ToolStripCharMapMenuItem(string character)
        {
            this.character = character;          
        }

        private ToolStripCharMapMenuItem()
        {

        }

        public event CharacterMapClickEventDelegate CharacterMapClickEvent = null;

        protected override void OnClick(EventArgs e)
        {
            if (CharacterMapClickEvent != null)
            {
                CharacterMapClickEvent(character);
            }

            base.OnClick(e);
        }
    }
}
