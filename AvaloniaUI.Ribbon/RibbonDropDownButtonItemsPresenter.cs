using Avalonia.Controls.Presenters;

using System;

namespace AvaloniaUI.Ribbon
{
    //public class RibbonDropDownItem : GalleryItem { }
    public class RibbonDropDownButtonItemsPresenter : ItemsPresenter
    {
        /*protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<RibbonDropDownItemPresenter>(this, RibbonDropDownItemPresenter.ContentProperty, RibbonDropDownItemPresenter.ContentTemplateProperty);
        }*/

        protected override Type StyleKeyOverride => typeof(ItemsPresenter);
    }
}
