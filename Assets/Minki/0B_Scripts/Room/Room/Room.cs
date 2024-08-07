using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    public CinemachineVirtualCamera v_cam;
    [SerializeField] private Portal _portal;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _backWall;

    [SerializeField] private string _stageName;

    public Room nextRoom;

    protected event Action OnActive;
    protected event Action OnCameraMoveEnd;

    public void Active() {
        _portal?.SetOwner(this);
        _backWall.SetActive(true); 
        v_cam.Priority = 15;

        StartCoroutine(CameraMoveEndCoroutine());

        OnActive?.Invoke();
    }

    private IEnumerator CameraMoveEndCoroutine() {
        yield return new WaitForSeconds(2f);

        OnCameraMoveEnd?.Invoke();
    }

    public virtual void Clear() {
        _frontWall.SetActive(false);
    }

    public void GotoNextStage() {
        if(nextRoom == null) return;

        nextRoom.Active();

        v_cam.Priority = 10;
    }

    [ContextMenu("Test")]
    private void Test() {
        Clear();
        GotoNextStage();
    }
}
