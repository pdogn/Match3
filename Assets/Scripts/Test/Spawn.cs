using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Transform Lefts, Rights, boardSlotNode;

    public int chessWidthNum;
    public int chessHeightNum;
    public int chessItemWidth;
    public int chessItemHeight;
    public int slotNum;

    public int ClearableNum;
    public int BlockTypeNum;
    public int LeftRandomBlocks;
    public int RightRandomBlocks;
    public int LevelNum;
    public int LevelBlockNum;

    public int blockTypeNum;
    public int blockBorderStep;

    

    void ABC()
    {
        int totalBlockNum = LeftRandomBlocks + RightRandomBlocks + LevelNum * LevelBlockNum;
        List<CardCon> blockArr = new List<CardCon>();
        for (int i = 0; i < totalBlockNum; i++)
        {
            blockArr.Add(new CardCon
            {
                id = i,
                oldX = 0,
                oldY = 0,
                level = 0,
                higherIds = new List<int>(),
                lowerIds = new List<int>()
            }) ;
        }
        //Left
        int pos = 0;
        for(int i=0; i< LeftRandomBlocks; i++)
        {
            GameObject obj = Instantiate(prefabs[0], Lefts);

            //obj.GetComponent<CardCon>().oldX -= i * 5;
            //obj.GetComponent<CardCon>().oldY = 0;
            blockArr[pos].oldX -= i * 5;
            blockArr[pos].oldY = 0;
            blockArr[pos].level -= i + 10; 

            int pre = pos - 1;
            while(pre >= 0)
            {
                //obj.GetComponent<CardCon>().higherIds.Add(pre);
                blockArr[pos].higherIds.Add(pre);
                pre--;
            }
            int next = pos + 1;
            while(next < LeftRandomBlocks)
            {
                //obj.GetComponent<CardCon>().lowerIds.Add(next);
                blockArr[pos].higherIds.Add(next);
                next++;
            }
            obj.GetComponent<CardCon>().Init(blockArr[pos]);
            pos++;
        }
        for (int i = 0; i < RightRandomBlocks; i++)
        {
            GameObject obj = Instantiate(prefabs[0], Rights);
            blockArr[pos].oldX += i * 5;
            blockArr[pos].oldY = 0;
            blockArr[pos].level -= i + 10;

            int pre = pos - 1;
            while (pre >= LeftRandomBlocks)
            {
                //obj.GetComponent<CardCon>().higherIds.Add(pre);
                blockArr[pos].higherIds.Add(pre);
                pre--;
            }
            int next = pos + 1;
            while (next < LeftRandomBlocks+RightRandomBlocks)
            {
                //obj.GetComponent<CardCon>().lowerIds.Add(next);
                blockArr[pos].higherIds.Add(next);
                next++;
            }
            obj.GetComponent<CardCon>().Init(blockArr[pos]);
            pos++;
        }
        ////
        bool isRandom = true;
        if(LeftRandomBlocks == 0
            && RightRandomBlocks ==0
            && LevelBlockNum % ClearableNum == 0
            && (LevelNum * LevelBlockNum) % (ClearableNum * blockTypeNum) == 0
            &LevelBlockNum <= 16)
        {
            isRandom = false;
        }
        //
        // Khởi tạo bàn cờ
        ChessBox chessBox = new ChessBox(chessWidthNum, chessHeightNum);
        List<CardCon> chessBlocks = new List<CardCon>();

        // Số lượng khối còn lại để xử lý
        int remainBlockNum = totalBlockNum - LeftRandomBlocks - RightRandomBlocks;
        int minWidth = 0;
        int maxWidth = chessWidthNum - 2;
        int minHeight = 0;
        int maxHeight = chessHeightNum - 2;

        // Khởi tạo các khối theo từng lớp
        for (int i = 0; i < LevelNum; i++)
        {
            int blockNum = Mathf.Min(LevelBlockNum, remainBlockNum);

            // Nếu đây là lớp cuối cùng
            if (LevelNum - 1 == i)
                blockNum = remainBlockNum;

            // Điều chỉnh biên theo logic
            if (blockBorderStep > 0 && i > 0)
            {
                switch (i % 4)
                {
                    case 0: minWidth += blockBorderStep; break;
                    case 3: maxWidth -= blockBorderStep; break;
                    case 2: minHeight += blockBorderStep; break;
                    case 1: maxHeight -= blockBorderStep; break;
                }
            }

            // Lấy khối từ danh sách
            var blocks = blockArr.GetRange(pos, blockNum);
            pos += blockNum;

            // Thiết lập tọa độ khối
            HashSet<string> blockPosSet = new HashSet<string>();
            for (int j = 0; j < blocks.Count; j++)
            {
                CardCon block = blocks[j];
                int nx, ny;
                string key;

                if (isRandom)
                {
                    // Tạo tọa độ ngẫu nhiên
                    do
                    {
                        nx = Random.Range(minWidth, maxWidth + 1);
                        ny = Random.Range(minHeight, maxHeight + 1);
                        key = $"{nx}_{ny}";
                    } while (blockPosSet.Contains(key));
                }
                else
                {
                    // Tạo tọa độ sắp xếp đều đặn
                    int sqrt = Mathf.FloorToInt(Mathf.Sqrt(LevelBlockNum));
                    nx = (j % sqrt) * 4 + Mathf.FloorToInt((chessWidthNum - 3 * sqrt) / 2);
                    if (sqrt % 2 == 0) nx -= 1;
                    ny = Mathf.FloorToInt(j / sqrt) * 5 + i;
                    key = $"{nx}_{ny}";
                }

                // Lưu tọa độ vào bàn cờ
                chessBox?.AddBlock(nx, ny, block);
                blockPosSet.Add(key);

                // Thiết lập mối quan hệ giữa các khối (gấp)
                int minX = Mathf.Max(nx - 2, 0);
                int minY = Mathf.Max(ny - 2, 0);
                int maxX = Mathf.Min(nx + 3, chessWidthNum - 2);
                int maxY = Mathf.Min(ny + 3, chessHeightNum - 2);

                int maxLevel = 0;
                for (int px = minX; px < maxX; px++)
                {
                    for (int py = minY; py < maxY; py++)
                    {
                        var nearbyBlocks = chessBox?.GetBlocks(px, py);
                        if (nearbyBlocks != null && nearbyBlocks.Count > 0)
                        {
                            var topBlock = nearbyBlocks[nearbyBlocks.Count - 1];
                            if (topBlock.id == block.id) continue;

                            maxLevel = Mathf.Max(maxLevel, topBlock.level);
                            block.lowerIds.Add(topBlock.id);
                            topBlock.higherIds.Add(block.id);
                        }
                    }
                }

                //block.BoardType = GameBoardEnum.Level;
                block.oldX = nx;
                block.oldY = ny;
                block.level = maxLevel + 1;
                block.oldX = nx * chessItemWidth;
                block.oldY = ny * chessItemHeight;

                // Thêm vào danh sách hiển thị
                chessBlocks.Add(block);
            }

            // Giảm số lượng khối còn lại
            remainBlockNum -= blockNum;
            if (remainBlockNum <= 0) break;
        }

        // Render các khối bàn cờ
        foreach (var block in chessBlocks)
        {
            GameObject node = Instantiate(prefabs[0], boardSlotNode);
            node.GetComponent<CardCon>().Init(block);
        }

        // Cập nhật kích thước các node mở rộng
        float width = slotNum * chessItemWidth * 3 + (slotNum + 1) * 5;
        //boardSlotNode.GetComponent<RectTransform>().sizeDelta = new Vector2(width, boardSlotNode.GetComponent<RectTransform>().sizeDelta.y);
        //boardLevelExtendNode.GetComponent<RectTransform>().sizeDelta = new Vector2(width, boardLevelExtendNode.GetComponent<RectTransform>().sizeDelta.y);

        // Điều chỉnh vị trí bảng cờ
        float x = -chessItemWidth * chessWidthNum / 2 + chessWidthNum;
        float y = chessItemHeight * chessHeightNum / 2 - 230;
        //boardLevelNode.localPosition = new Vector3(x, y, 0);
        Instantiate(prefabs[0], boardSlotNode);
    }
}
public class ChessBox
{
    private List<CardCon>[,] grid;

    public ChessBox(int width, int height)
    {
        grid = new List<CardCon>[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid[i, j] = new List<CardCon>();
            }
        }
    }

    public void AddBlock(int x, int y, CardCon block)
    {
        grid[x, y].Add(block);
    }

    public List<CardCon> GetBlocks(int x, int y)
    {
        return grid[x, y];
    }
}

