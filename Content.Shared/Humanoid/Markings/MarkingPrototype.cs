using Content.Shared.Humanoid.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Array; // AuroraSong
using Robust.Shared.Utility;

namespace Content.Shared.Humanoid.Markings
{
    [Prototype]
    // AuroraSong: Make markings inheriting (IInheritingPrototype)
    public sealed partial class MarkingPrototype : IPrototype, IInheritingPrototype
    {
        [IdDataField]
        public string ID { get; private set; } = "uwu";

        // AuroraSong: Make markings inheriting
        [ParentDataField(typeof(AbstractPrototypeIdArraySerializer<MarkingPrototype>))]
        public string[]? Parents { get; }

        [NeverPushInheritance]
        [AbstractDataField]
        public bool Abstract { get; }
        // End AuroraSong

        public string Name { get; private set; } = default!;

        [DataField("bodyPart", required: true)]
        public HumanoidVisualLayers BodyPart { get; private set; }

        [DataField("markingCategory", required: true)]
        public MarkingCategories MarkingCategory { get; private set; }

        [DataField("speciesRestriction")]
        public List<string>? SpeciesRestrictions { get; private set; }

        // DEN - Invert marking restrictions
        [DataField]
        public bool InvertSpeciesRestriction { get; private set; }

        [DataField]
        public Sex? SexRestriction { get; private set; }

        // DEN - Invert marking restrictions
        [DataField]
        public bool InvertSexRestriction { get; private set; }

        [DataField]
        public bool FollowSkinColor { get; private set; }

        [DataField]
        public bool ForcedColoring { get; private set; }

        [DataField]
        public MarkingColors Coloring { get; private set; } = new();

        /// <summary>
        /// Do we need to apply any displacement maps to this marking? Set to false if your marking is incompatible
        /// with a standard human doll, and is used for some special races with unusual shapes
        /// </summary>
        [DataField]
        public bool CanBeDisplaced { get; private set; } = true;

        [DataField("sprites", required: true)]
        public List<SpriteSpecifier> Sprites { get; private set; } = default!;

        // impstation edit - allow markings to support shaders
        [DataField("shader")]
        public string? Shader { get; private set; } = null;
        // end impstation edit

        /// <summary>
        /// Allows specific images to be put into any arbitrary layer on the mob.
        /// Whole point of this is to have things like tails be able to be
        /// behind the mob when facing south-east-west, but in front of the mob
        /// when facing north. This requires two+ sprites, each in a different
        /// layer.
        /// Is a dictionary: sprite name -> layer name,
        /// e.g. "tail-cute-vulp" -> "tail-back", "tail-cute-vulp-oversuit" -> "tail-oversuit"
        /// also, FLOOF ADD =3
        /// </summary>
        [DataField]
        public Dictionary<string, string>? Layering { get; private set; }

        /// <summary>
        /// Allows you to link a specific sprite's coloring to another sprite's coloring.
        /// This is useful for things like tails, which while they have two sets of sprites,
        /// the two sets of sprites should be treated as one sprite for the purposes of
        /// coloring. Just more intuitive that way~
        /// Format: spritename getting colored -> spritename which colors it
        /// so if we have a Tail Behind with 'cooltail' as the sprite name, and a Tail Oversuit
        /// with 'cooltail-oversuit' as the sprite name, and we want to have the Tail Behind
        /// inherit the color of the Tail Oversuit, we would do:
        /// cooltail -> cooltail-oversuit
        /// cooltail will be hidden from the color picker, and just use whatevers set for
        /// cooltail-oversuit. Easy huh?
        /// also, FLOOF ADD =3
        /// </summary>
        [DataField]
        public Dictionary<string, string>? ColorLinks { get; private set; }

        // Aurora Song: Sort markings to the top for preferred species.

        /// <summary>
        /// A list of species IDs that will prefer to use this marking above others.
        /// Species in this list will have this marking sorted to the top, making them more accessible.
        /// In the future, if marking randomization is added, those will probably use this list too for cohesion.
        /// </summary>
        /// <remarks>
        /// For example: Imagine humans have various ear markings, ranging from regular humanoid ears, to
        /// pointy elf/imp-like ears, to kemonomimi traits that may overlap with other species such as
        /// vulpkanin or tajaran. The humanoid and elf ears may be preferred by humans, but their kemonomimi
        /// ears will be preferred by vulpkanin or tajaran respectively. This floats the elf/humanoid ears to the
        /// top of humans' ear marking lists.
        /// </remarks>
        [DataField]
        public HashSet<ProtoId<SpeciesPrototype>>? PreferredSpecies = null;

        // End Aurora Song

        public Marking AsMarking()
        {
            return new Marking(ID, Sprites.Count);
        }
    }
}
