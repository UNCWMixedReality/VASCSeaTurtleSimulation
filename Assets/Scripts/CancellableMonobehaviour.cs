using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// This was broken by Blake Blackport to fix build errors
public class CancellableMonobehaviour : MonoBehaviour
{
    protected CancellationTokenSource tokenSource = new CancellationTokenSource();
    protected CancellationToken token;
    List<CancellationTokenSource> tokens = new List<CancellationTokenSource>();

    public virtual void Awake()
    {
        token = tokenSource.Token;
        // UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    // private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange playModeState)
    private void OnPlayModeStateChanged()
    {
        // if (playModeState.Equals(UnityEditor.PlayModeStateChange.ExitingPlayMode))
        CancelTokens();
    }

    void CancelTokens()
    {
        foreach (var token in tokens)
            token.Cancel();
        tokenSource.Cancel();
    }

    public virtual void OnDestroy()
    {
        CancelTokens();
    }

    public CancellationToken GetToken()
    {
        token.ThrowIfCancellationRequested();
        return token;
    }

    public CancellationToken LinkToken(CancellationToken _token)
    {
        if (_token == token)
            return _token;
        var childToken = CancellationTokenSource.CreateLinkedTokenSource(_token);
        tokens.Add(childToken);
        return childToken.Token;
    }
}
