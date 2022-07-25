// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerToys.Settings.UI.Library.ViewModels
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Small classes")]
    public interface IPluginAdditionalOptionViewModel
    {
        string DisplayDescription { get; }

        string DisplayLabel { get; }

        event PropertyChangedEventHandler PropertyChanged;
    }

    public static class PluginAdditionalOptionViewModelFactory
    {
        public static IPluginAdditionalOptionViewModel Create(IPluginAdditionalOption additionalOption)
        {
            IPluginAdditionalOptionViewModel result = null;

            switch (additionalOption)
            {
                case PluginAdditionalOptionEnum optionEnum:
                    result = new PluginAdditionalOptionViewModelEnum(optionEnum);
                    break;
                case PluginAdditionalOptionInt optionInt:
                    result = new PluginAdditionalOptionViewModelInt(optionInt);
                    break;
                case PluginAdditionalOptionBool optionBool:
                    result = new PluginAdditionalOptionViewModelBool(optionBool);
                    break;
            }

            return result;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Small classes")]
    public abstract class PluginAdditionalOptionViewModel<TOption, TValue> : INotifyPropertyChanged, IPluginAdditionalOptionViewModel
        where TOption : PluginAdditionalOption<TValue>
    {
        protected TOption AdditionalOption { get; init; }

        internal PluginAdditionalOptionViewModel(TOption additionalOption)
        {
            AdditionalOption = additionalOption;
        }

        public string DisplayLabel => AdditionalOption.DisplayLabel;

        public string DisplayDescription => AdditionalOption.DisplayDescription;

        public TValue Value
        {
            get => AdditionalOption.Value;
            set
            {
                if (!AdditionalOption.Value.Equals(value))
                {
                    AdditionalOption.Value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Small classes")]
    public class PluginAdditionalOptionViewModelBool : PluginAdditionalOptionViewModel<PluginAdditionalOptionBool, bool>
    {
        internal PluginAdditionalOptionViewModelBool(PluginAdditionalOptionBool additionalOption)
            : base(additionalOption)
        {
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Small classes")]
    public class PluginAdditionalOptionViewModelInt : PluginAdditionalOptionViewModel<PluginAdditionalOptionInt, int>
    {
        internal PluginAdditionalOptionViewModelInt(PluginAdditionalOptionInt additionalOption)
            : base(additionalOption)
        {
        }

        public int MinValue => AdditionalOption.MinValue;

        public int MaxValue => AdditionalOption.MaxValue;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Small classes")]
    public class PluginAdditionalOptionViewModelEnum : PluginAdditionalOptionViewModel<PluginAdditionalOptionEnum, int>
    {
        internal PluginAdditionalOptionViewModelEnum(PluginAdditionalOptionEnum additionalOption)
            : base(additionalOption)
        {
        }

        public IReadOnlyCollection<string> ValueLabels => AdditionalOption.ValueLabels;

        public IReadOnlyCollection<string> ValueDescriptions => AdditionalOption.ValueDescriptions;
    }
}
