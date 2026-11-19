

namespace GettingRidOfIt
{
    public class BloodStain : InteractItem
    {


        private void OnEnable()
        {
            GameEvents.OnHourPassed += HourPassed;
        }

        private void OnDisable()
        {
            GameEvents.OnHourPassed -= HourPassed;
        }

        private void HourPassed()
        {
            GameManager.Instance.SuspicionChange(itemSo.decompositionPerHour);
        }


        public override void DefaultInteract(bool onOrOff)
        {
            Destroy(gameObject);
        }

        public override Body GetBody() { return null; }

        public override BodyPart GetBodyPart() { return null; }

        public override Holder GetHolder() { return null; }

        public override InteractItem GetItem() { return this; }
    }
}
