using UnityEngine;

namespace Build3D
{
    public class Item3D : ObjectSprite3D
    {
        public bool SelectEnabled = true;
        public bool ScaleEnabled = true;
        public bool TranslationEnabled = true;
        public bool RotationEnabled = true;

        public GameObject relationBox;

        public override AssetVO VO
        {
            set
            {
                base.VO = value;

                this.gameObject.name = "Item3 " + _vo.id;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            Destroy(relationBox);
        }
    }
}
