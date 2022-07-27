// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.PowerToys.Settings.UI.Library
{
    public class PluginAdditionalOptionInt : PluginAdditionalOption<int>
    {
        public int MinValue { get; init; }

        public int MaxValue { get; init; }
    }
}
