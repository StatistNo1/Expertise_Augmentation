namespace ExpertiseAugmentation {
    public class EA_ResearchMod : Verse.ResearchMod {
        public int IncreaseLevelBy;
        public int MaxLevel;
        public System.Collections.Generic.List<Verse.ThingDef> MechsToAugment;
        public System.Collections.Generic.List<Verse.ResearchProjectDef> ResearchDefs;
        private readonly int BaseLevel = 10;
        public override void Apply() {
            SetSkillLevel(CountFinishedResearchDefs());
        }
        public int CountFinishedResearchDefs() {
            int finishedCount = 0;
            foreach (Verse.ResearchProjectDef rpd in ResearchDefs)
                if (rpd.IsFinished)
                    finishedCount++;
            return finishedCount;
        }
        public void SetSkillLevel(int finishedCount) {
            foreach (Verse.ThingDef td in MechsToAugment)
                if (BaseLevel + (IncreaseLevelBy * finishedCount) > MaxLevel)
                    td.race.mechFixedSkillLevel = MaxLevel;
                else
                    td.race.mechFixedSkillLevel = BaseLevel + (IncreaseLevelBy * finishedCount);
        }
    }
    public class GameComponent : Verse.GameComponent {
        public GameComponent(Verse.Game game) {}
        public override void FinalizeInit() {
            base.FinalizeInit();
            DefOf.EA_Research_1.ReapplyAllMods();
        }
    }

    [RimWorld.DefOf]
    public static class DefOf {
        public static Verse.ResearchProjectDef EA_Research_1;
        static DefOf() {
            RimWorld.DefOfHelper.EnsureInitializedInCtor(typeof(Verse.ResearchProjectDef));
        }
    }
}
