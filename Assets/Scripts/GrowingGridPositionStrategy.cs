using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrowingGridPositionStrategy : PositionStrategy {

    private IEnumerator<Vector3> iter;


	void Awake() {
		iter = CreateIterator();
		// If we wanted to properly expose a "HasNext" value, we'd need to call
		// MoveNext after creating the iter.
	}

	public override bool HasNext() {
		// We can grow forever.
		return true;
	}

    public override Vector3 NextPosition() {
        iter.MoveNext();
        return iter.Current;
    }

    class IntVector2 {
        public int x;
        public int y;
        public static IntVector2 zero = new IntVector2(0,0);
        public IntVector2(int x_in, int y_in) {
            x = x_in;
            y = y_in;
        }
    }

    IEnumerator<Vector3> CreateIterator() {
        //  Grow like this:
        //  16 15 14 13
        //  05 06 07 12
        //  04 03 08 11
        //  01 02 09 10
        //
        //  ^- <- <- <.
        //  .> -> v   ^
        //  ^- <. v   ^
        //  -> -^ .> -^

        IntVector2 extents = IntVector2.zero;
        Vector3 last_pos = Vector3.zero;
        yield return last_pos;

        int length = 0;
        while (true) {
            // go right 1
            ++extents.x;
            ++last_pos.x;
            yield return last_pos;

            // go up length+1
            ++extents.y;
            length = extents.y;
            for (int i = 0; i < length; ++i) {
                ++last_pos.y;
                yield return last_pos;
            }

            // go left length
            length = extents.x;
            for (int i = 0; i < length; ++i) {
                --last_pos.x;
                yield return last_pos;
            }

            // go up 1
            ++extents.y;
            ++last_pos.y;
            yield return last_pos;

            // go right length+1
            ++extents.x;
            length = extents.x;
            for (int i = 0; i < length; ++i) {
                ++last_pos.x;
                yield return last_pos;
            }

            // go down length
            length = extents.y;
            for (int i = 0; i < length; ++i) {
                --last_pos.y;
                yield return last_pos;
            }
        }
    }
}
