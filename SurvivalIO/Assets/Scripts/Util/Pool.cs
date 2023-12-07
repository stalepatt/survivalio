using UnityEngine;
using UnityEngine.Pool;
public class Pool<T> where T : Component
{
    public GameObject Root { get { return _container; } }
    private GameObject _container;
    protected ObjectPool<T> _pool;
    public T Get() => _pool.Get();
    public void Release(T element) => _pool.Release(element);
    public void Init(GameObject container, GameObject parentObject = null)
    {
        _container = container;
        if (parentObject != null)
        {
            _container.transform.SetParent(parentObject.transform);
        }
        _pool = new(Create, OnGet, OnRelease, OnDestroy); // new 연산자 명시적으로 수정
    }

    protected virtual T Create()
    {
        return Managers.ResourceManager.Instantiate($"Ingame/{typeof(T)}", _container.transform).GetOrAddComponent<T>();
    }
    protected virtual void OnGet(T element) => element.gameObject.SetActive(true);

    protected virtual void OnRelease(T element) => element.gameObject.SetActive(false);

    protected virtual void OnDestroy(T element) => Object.Destroy(element.gameObject);
}