﻿/*
 
    LibertyV - Viewer/Editor for RAGE Package File version 7
    Copyright (C) 2013  koolk <koolkdev at gmail.com>
   
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
  
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
   
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibertyV.Rage.Resources.Types
{
    class ClassTypeInfo : TypeInfo
    {
        public List<Tuple<string, TypeInfo>> Members = new List<Tuple<string, TypeInfo>>();

        protected ClassTypeInfo(string name)
            : base(name)
        {
        }

        protected void AddMember(string name, TypeInfo type)
        {
            this.Members.Add(Tuple.Create(name, type));
        }

        protected void AddMember(string name, string type)
        {
            TypeInfo typeinfo = TypesCache.GetTypeInfoByName(type);
            if (typeinfo == null)
            {
                throw new ArgumentException(String.Format("Couldn't find the type {0}", type));
            }
            this.Members.Add(Tuple.Create(name, typeinfo));
        }

        public override ResourceObject Create()
        {
            Dictionary<string, ResourceObject> values = new Dictionary<string, ResourceObject>();
            foreach (var member in this.Members)
            {
                values[member.Item1] = member.Item2.Create();
            }
            return new ClassObject(this, values);
        }

        public override ResourceObject Create(ResourceReader reader)
        {
            Dictionary<string, ResourceObject> values = new Dictionary<string, ResourceObject>();
            foreach (var member in this.Members)
            {
                values[member.Item1] = member.Item2.Create(reader);
            }
            return new ClassObject(this, values);
        }
    }
}
