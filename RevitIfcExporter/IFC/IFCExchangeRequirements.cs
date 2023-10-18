﻿//
// BIM IFC export alternate UI library: this library works with Autodesk(R) Revit(R) to provide an alternate user interface for the export of IFC files from Revit.
// Copyright (C) 2013  Autodesk, Inc.
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// ref: https://github.com/Autodesk/revit-ifc/tree/9a6fc912fb8510647f9d2bcc0d57b0451a2194b6/Source/Revit.IFC.Common/Enums
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Revit.IFC.Common.Enums;

namespace BIM.IFC.Export
{
    class IFCExchangeRequirements
    {
        /// <summary>
        /// The list of Known Exchange Requirements
        /// </summary>
        static IDictionary<IFCVersion, IList<KnownERNames>> KnownExchangeRequirements = new Dictionary<IFCVersion, IList<KnownERNames>>();
        static IDictionary<IFCVersion, IList<string>> KnownExchangeRequirementsLocalized = new Dictionary<IFCVersion, IList<string>>();

        static void Initialize()
        {
            if (KnownExchangeRequirements.Count == 0)
            {
                // For IFC2x3 CV2.0
                IFCVersion ifcVersion = IFCVersion.IFC2x3CV2;
                KnownExchangeRequirements.Add(ifcVersion, new List<KnownERNames>() { KnownERNames.Architecture, KnownERNames.BuildingService, KnownERNames.Structural });
                List<string> erNameListForUI = new List<string>(KnownExchangeRequirements[ifcVersion].Select(x => x.ToFullLabel()));
                KnownExchangeRequirementsLocalized.Add(ifcVersion, erNameListForUI);

                // For IFC4RV
                ifcVersion = IFCVersion.IFC4RV;
                KnownExchangeRequirements.Add(ifcVersion, new List<KnownERNames>() { KnownERNames.Architecture, KnownERNames.BuildingService, KnownERNames.Structural });
                KnownExchangeRequirementsLocalized.Add(ifcVersion, erNameListForUI);
            }
        }

        /// <summary>
        /// Get the list of known Exchange Requirements
        /// </summary>
        public static IDictionary<IFCVersion, IList<KnownERNames>> ExchangeRequirements
        {
            get
            {
                Initialize();
                return KnownExchangeRequirements;
            }
        }

        /// <summary>
        /// Get list of ER names for UI based on the given IFC Version
        /// </summary>
        /// <param name="ifcVers">The IFC Version</param>
        /// <returns>The List of known ER</returns>
        public static IList<string> ExchangeRequirementListForUI(IFCVersion ifcVers)
        {
            Initialize();
            return KnownExchangeRequirementsLocalized.FirstOrDefault(x => x.Key == ifcVers).Value;
        }

        /// <summary>
        /// Parse the Exchange Requirement (ER) name string into the associated Enum
        /// </summary>
        /// <param name="erName">The ER Name</param>
        /// <returns>The ER enum</returns>
        public static KnownERNames ParseEREnum(string erName)
        {
            if (Enum.TryParse(erName, out KnownERNames erEnum))
            {
                return erEnum;
            }

            return KnownERNames.NotDefined;
        }
    }
}