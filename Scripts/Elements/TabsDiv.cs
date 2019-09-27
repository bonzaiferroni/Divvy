using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
    public class TabsDiv : Div
    {
        private ToggleDiv ToggleDiv { get; set; }
        private PageDiv PageDiv { get; set; }

        protected override void Construct()
        {
            base.Construct();
            ToggleDiv = null;
            PageDiv = null;
        }

        protected override void Associate()
        {
            base.Associate();
            if (!ToggleDiv || !PageDiv) throw new DivvyException("TabsDiv requires ToggleDiv and PageDiv children");
            InitVisible();
        }

        private void InitVisible()
        {
            for (int i = 0; i < ToggleDiv.Toggles.Count; i++)
            {
                var toggle = ToggleDiv.Toggles[i];
                PageDiv.ShowPage(i, toggle.IsOn, true);
            }
        }

        public override void AddChild(IElement child, int index = -1, bool instantPositioning = true)
        {
            base.AddChild(child, index, instantPositioning);
            if (child is ToggleDiv toggles)
            {
                if (ToggleDiv) throw new DivvyException("TabsDiv can only have one ToggleDiv");
                ToggleDiv = toggles;
                ToggleDiv.OnToggleChanged += OnToggleChange;
            }

            if (child is PageDiv pages)
            {
                if (PageDiv) throw new DivvyException("TabsDiv can only have on PagesDiv");
                PageDiv = pages;
                PageDiv.OnPageChange += OnPageChange;
            }
        }

        public override void RemoveChild(IElement child)
        {
            base.RemoveChild(child);
            if (ReferenceEquals(ToggleDiv, child) || ReferenceEquals(PageDiv, child))
                throw new DivvyException("TabsDiv cannot remove dependency");
        }

        private void OnToggleChange(ToggleElement context, bool isOn, int index)
        {
            PageDiv.ShowPage(index, isOn);
            if (isOn && Application.isPlaying)
            {
                // context.Transform.SetAsLastSibling();
            }
        }

        private void OnPageChange(IElement element, bool isvisible, int index)
        {
            // todo: reverse control
        }
    }
}