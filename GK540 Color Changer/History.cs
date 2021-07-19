using System.Collections.Generic;
using System.Drawing;

namespace GK540_Color_Changer {
public class History {
    private const int MaxSize = 100;
    private readonly LinkedList<HistoryEvent> _list = new LinkedList<HistoryEvent>();

    public void Add(int keyNumber, Color oldColor) {
        _list.AddFirst(new HistoryEvent(keyNumber, oldColor));
        TrimList();
    }

    public void Add(HistoryEvent historyEvent) {
        _list.AddFirst(historyEvent);
        TrimList();
    }

    private void TrimList() {
        if (_list.Count > MaxSize)
            _list.RemoveLast();
    }

    public HistoryEvent GetLastEvent() {
        LinkedListNode<HistoryEvent> node = _list?.First;
        HistoryEvent lastEvent = node?.Value;

        if (node is not null && lastEvent is not null)
            _list.RemoveFirst();

        return lastEvent;
    }

    public void Clear() {
        _list.Clear();
    }
}
}