
namespace Assets.IntenseTPS.Scripts.Level
{
    public class LootBoxes : Interactive
    {
        public string showName;
        public bool needSerializeToFile = true;
        public string inventoryFileName = "null";
        public bool isTransaction = false;
        public string transactionName = "null";
        private int _id = -1;

        private void Start()
        {
            canUes = true;
            _id = LevelManager.LootBoxStart(transform, showName, needSerializeToFile, inventoryFileName, isTransaction, transactionName);
        }
        public override void Use()
        {
            if(_id>=0)
                LevelManager.LootBoxUse(_id);
        }
    }
}
