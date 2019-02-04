 
using System.Collections;
using UnityEngine;
 
class Dragger : MonoBehaviour
{
    public Color mouseOverColor = Color.blue;
    public Color originalColor = Color.yellow;
    public bool dragging = false;
    public Vector3 delta = new Vector3(0F, 0F, 0F);
    public Cell rightCell;

    void OnMouseDown()
    {
        Debug.Log("asdasdsdasdasd");
        dragging = true;
        delta = this.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (rightCell)
        {
            rightCell.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
 
    void OnMouseUp()
    {
        dragging = false;
        if ( !GameObject.Find("Grid").GetComponent<GridInit>().DropBlock(this.gameObject) ){
            GameObject.Find("Inventory").GetComponent<Inventory>().FailDropBlock(this.gameObject);
        }
        else{
            GameObject.Find("Inventory").GetComponent<Inventory>().SuccedDropBlock(this.gameObject);
        };
    }
 
    void Update()
    {
        if (dragging)
        {
			this.transform.MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition) + delta, 0);
        }
    }

    public void SetRightCell(Cell cell)
    {
        rightCell = cell;
        GameObject obj = rightCell.gameObject;

		if (rightCell.isRightCell) {
			this.transform.Translate( obj.transform.position.x - this.transform.position.x, obj.transform.position.y - this.transform.position.y, 0F);
			rightCell.isRightCell = false;
			obj.GetComponent<CircleCollider2D>().enabled = false;
		}
    }
}