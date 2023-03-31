using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Core;

public ref struct EntityGlobalsEnumerator<TGlobal> where TGlobal : GlobalType<TGlobal>
{
	private readonly TGlobal[] baseGlobals;
	private readonly TGlobal[] entityGlobals;
	private int i;
	private TGlobal current;

	public EntityGlobalsEnumerator(TGlobal[] baseGlobals, TGlobal[] entityGlobals)
	{
		this.baseGlobals = baseGlobals;
		this.entityGlobals = entityGlobals;
		i = 0;
		current = null;
	}

	public TGlobal Current => current;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool MoveNext()
	{
		if (entityGlobals == null)
			return false;

		while (i < baseGlobals.Length) {
			current = baseGlobals[i++];
			var entitySlot = current.Index.PerEntityIndex;
			if (entitySlot < 0)
				return true;

			current = entityGlobals[entitySlot];
			if (current != null)
				return true;
		}
		return false;
	}

	public EntityGlobalsEnumerator<TGlobal> GetEnumerator() => this;
}