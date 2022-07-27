// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Microsoft.PowerToys.Settings.UI.Library
{
    [JsonConverter(typeof(PluginAdditionalOptionConverter))]
    public interface IPluginAdditionalOption
    {
        public string Key { get; }

        public string DisplayLabel { get; }

        /// <summary>
        /// Gets a value to show a description of this setting in the settings ui. (Optional)
        /// </summary>
        public string DisplayDescription { get; }

        public object AnyValue { get; }

        public void CopyValue(IPluginAdditionalOption other);
    }
}
