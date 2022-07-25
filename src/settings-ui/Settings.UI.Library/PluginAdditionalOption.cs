// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Microsoft.PowerToys.Settings.UI.Library
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Very small classes")]
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

    public abstract class PluginAdditionalOptionBase : IPluginAdditionalOption
    {
        public string Key { get; set; }

        public string DisplayLabel { get; set; }

        /// <summary>
        /// Gets or sets a value to show a description of this setting in the settings ui. (Optional)
        /// </summary>
        public string DisplayDescription { get; set; }

        public abstract object AnyValue { get; }

        public abstract void CopyValue(IPluginAdditionalOption other);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Very small classes")]
    public abstract class PluginAdditionalOption<T> : PluginAdditionalOptionBase
    {
        public T Value { get; set; }

        public override object AnyValue => Value;

        public override void CopyValue(IPluginAdditionalOption other)
        {
            if (other is PluginAdditionalOption<T> otherOption)
            {
                Value = otherOption.Value;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Very small classes")]
    public class PluginAdditionalOptionBool : PluginAdditionalOption<bool>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Very small classes")]
    public class PluginAdditionalOptionInt : PluginAdditionalOption<int>
    {
        public int MinValue { get; init; }

        public int MaxValue { get; init; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Very small classes")]
    public class PluginAdditionalOptionEnum : PluginAdditionalOptionInt
    {
        public IReadOnlyCollection<string> ValueLabels { get; init; }

        public IReadOnlyCollection<string> ValueDescriptions { get; init; }
    }
}
