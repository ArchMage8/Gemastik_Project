using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class LevelSelectSystem : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject targetImage;
    [SerializeField] private int targetScene;
    private Vector3 targetPos;
    public LevelSelectManager manager;

    private float moveSpeed = 3f;

    private void Start()
    {
        targetPos = transform.position;
        targetImage.SetActive(false);
    }

    public void ToggleImage()
    {
        if (!manager.isPreviewing)
        {
            manager.isPreviewing = true;
           
            StartCoroutine(enableHandler());
            
        }
    }

    public void CloseImage()
    {
        if(manager.isPreviewing)
        {
           StartCoroutine(disableHandler());
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(targetScene);
    }

    private IEnumerator enableHandler()
    {
        ChangeZoom(manager.targetZoom);
        Vector3 tempPos = new Vector3(targetPos.x + manager.XOffset, targetPos.y, targetPos.z);
        MoveCamera(tempPos);
        
        yield return new WaitForSeconds(1.5f);
        targetImage.SetActive(true);

    }

    private IEnumerator disableHandler()
    {
        ChangeZoom(manager.initialZoom);
        
        mainCamera.orthographicSize = manager.initialZoom;
        MoveCamera(manager.camInitial);
        yield return new WaitForSeconds(0.5f);
        targetImage.SetActive(false);
       
       
        manager.isPreviewing = false;
    }


    public void MoveCamera(Vector3 targetPosition)
    {
        float destination = targetPosition.x;
        StartCoroutine(MoveCameraCoroutine(destination, moveSpeed));
    }

    private IEnumerator MoveCameraCoroutine(float targetX, float speed)
    {
        Vector3 currentPos = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(targetX, currentPos.y, currentPos.z);

        while (Mathf.Abs(mainCamera.transform.position.x - targetX) > 0.01f)
        {
            float newX = Mathf.Lerp(mainCamera.transform.position.x, targetX, speed * Time.unscaledDeltaTime);
            mainCamera.transform.position = new Vector3(newX, currentPos.y, currentPos.z);
            yield return null;
        }
        mainCamera.transform.position = targetPosition; // Ensure the camera reaches the exact position
    }

    private void ChangeZoom(float targetSize)
    {
        StartCoroutine(ZoomCoroutine(targetSize));
    }

    private IEnumerator ZoomCoroutine(float targetSize)
    {
        float startSize = mainCamera.orthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < manager.Zoomrate)
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime/manager.Zoomrate);
            elapsedTime += Time.deltaTime;
            yield return null;
          
        }

        mainCamera.orthographicSize = targetSize;
    }

}
