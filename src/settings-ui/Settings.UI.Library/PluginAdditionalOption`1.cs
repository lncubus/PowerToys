// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.PowerToys.Settings.UI.Library
{
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
}
