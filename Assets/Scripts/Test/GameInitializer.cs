//using System.Collections.Generic;
//using UnityEngine;

//public class BlockType
//{
//    public int Id;
//    public int X;
//    public int Y;
//    public int Width;
//    public int Height;
//    public int Level;
//    public string BoardType;
//    public string Content;
//    // 被哪些block压着
//    public List<int> HigherIds;
//    // 压着哪些block
//    public List<int> LowerIds;
//}

//public class LevelType
//{
//    public int chessWidthNum;
//    public int chessHeightNum;
//    public int chessItemWidth;
//    public int chessItemHeight;
//    public int slotNum;

//    public int ClearableNum;
//    public int BlockTypeNum;
//    public int LeftRandomBlocks;
//    public int RightRandomBlocks;
//    public int LevelNum;
//    public int LevelBlockNum;

//    public int blockTypeNum;
//    public int blockBorderStep;
//}
//public class GameInitializer : MonoBehaviour
//{
//    public GameObject[] blockPrefabs; // Prefabs cho khối
//    public Transform boardRandomLeftNode, boardRandomRightNode, boardSlotNode, boardLevelExtendNode, boardLevelNode;


//    //public int ClearableNum;
//    //public int BlockTypeNum;
//    //public int LeftRandomBlocks;
//    //public int RightRandomBlocks;
//    //public int LevelNum;
//    //public int LevelBlockNum;
//    public void InitGame(int num)
//    {
//        //// Lấy dữ liệu level
//        LevelType currentLevel = new LevelType();// = DataManager.Instance.GetLevel(num - 1);
//        currentLevel.chessWidthNum = 20;
//        currentLevel.chessHeightNum = 20;
//        currentLevel.chessItemWidth = 22;
//        currentLevel.chessItemHeight = 22;

//        currentLevel.slotNum = 7;
//        currentLevel.ClearableNum = 3;
//        currentLevel.LeftRandomBlocks = 4;
//        currentLevel.RightRandomBlocks = 4;
//        currentLevel.LevelNum = 4;
//        currentLevel.LevelBlockNum = 9;
//        currentLevel.blockTypeNum = 6;
//        currentLevel.blockBorderStep = 1;
//        //if (currentLevel == null)
//        //{
//        //    if (DataManager.Instance.Levels.Count > 0)
//        //    {
//        //        num = 1;
//        //        DataManager.Instance.Level = num;
//        //        currentLevel = DataManager.Instance.GetLevel(0);
//        //    }
//        //    else
//        //    {
//        //        return;
//        //    }
//        //}

//        //ClearNodeHistory();
//        //DataManager.Instance.CurrentLevel = currentLevel;
//        //Debug.Log($"Level {num}");

//        // Tính tổng số khối
//        int blockUnit = currentLevel.ClearableNum * currentLevel.BlockTypeNum;
//        int totalBlockNum = currentLevel.LeftRandomBlocks + currentLevel.RightRandomBlocks
//            + currentLevel.LevelNum * currentLevel.LevelBlockNum;
//        //int blockUnit = ClearableNum * BlockTypeNum;
//        //int totalBlockNum = LeftRandomBlocks + RightRandomBlocks
//        //    + LevelNum * LevelBlockNum;

//        if (totalBlockNum % blockUnit != 0)
//        {
//            totalBlockNum = (Mathf.FloorToInt(totalBlockNum / blockUnit) + 1) * blockUnit;
//        }

//        // Tạo danh sách khối
//        List<BlockType> blockArr = new List<BlockType>();
//        //List<string> contentArr = new List<string>();
//        //var contentTarget = currentLevel.BlockContentArr.GetRange(0, currentLevel.BlockTypeNum);
//        //for (int i = 0; i < totalBlockNum; i++)
//        //{
//        //    contentArr.Add(contentTarget[i % currentLevel.BlockTypeNum]);
//        //}

//        //contentArr = Shuffle(contentArr);
//        for (int i = 0; i < totalBlockNum; i++)
//        {
//            //BlockType x = new BlockType();
//            //x.Id = i;
//            //x.X = 0;
//            //x.Y = 0;
//            //x.Width = currentLevel.chessItemWidth * 3;
//            //x.Height = currentLevel.chessItemHeight * 3;
//            //x.Level = 0;
//            //x.BoardType = "";
//            //x.Content = "conten -- " + i;
//            //blockArr.Add(x);
//            blockArr.Add(new BlockType {
//                Id = i,
//                X = 0,
//                Y = 0,
//                Width = currentLevel.chessItemWidth * 3,
//                Height = currentLevel.chessItemHeight * 3,
//                Level = 0,
//                BoardType = null,
//                Content = "conten -- " + i,
//                HigherIds = new List<int>(),
//                LowerIds = new List<int>()
//            }) ;
//    }

//        // Biến đếm vị trí
//        int pos = 0;

//        // Khởi tạo các khối ngẫu nhiên bên trái
//        for (int i = 0; i < currentLevel.LeftRandomBlocks; i++)
//        {
//            GameObject node = Instantiate(blockPrefabs[0], boardRandomLeftNode);
//            //blockArr[pos].BoardType = GameBoardEnum.RandomLeft;
//            blockArr[pos].X -= i * 5;
//            blockArr[pos].Y = 0;
//            blockArr[pos].Level -= i + 10;

//            // Gấp khối
//            for (int pre = pos - 1; pre >= 0; pre--)
//            {
//                blockArr[pos].HigherIds.Add(pre);
//            }
//            for (int next = pos + 1; next < currentLevel.LeftRandomBlocks; next++)
//            {
//                blockArr[pos].LowerIds.Add(next);
//            }

//            node.GetComponent<CardCon>().Init(blockArr[pos]);
//            pos++;
//        }

//        // Khởi tạo các khối ngẫu nhiên bên phải
//        for (int i = 0; i < currentLevel.RightRandomBlocks; i++)
//        {
//            GameObject node = Instantiate(blockPrefabs[1], boardRandomRightNode);
//            //blockArr[pos].BoardType = GameBoardEnum.RandomRight;
//            blockArr[pos].X += i * 5;
//            blockArr[pos].Y = 0;
//            blockArr[pos].Level -= i + 10;

//            for (int pre = pos - 1; pre >= currentLevel.LeftRandomBlocks; pre--)
//            {
//                blockArr[pos].HigherIds.Add(pre);
//            }
//            for (int next = pos + 1; next < currentLevel.LeftRandomBlocks + currentLevel.RightRandomBlocks; next++)
//            {
//                blockArr[pos].LowerIds.Add(next);
//            }

//            node.GetComponent<CardCon>().Init(blockArr[pos]);
//            pos++;
//        }

//        // Kiểm tra điều kiện để sắp xếp theo quy luật
//        bool isRandom = !(currentLevel.LeftRandomBlocks == 0
//                          && currentLevel.RightRandomBlocks == 0
//                          && currentLevel.LevelBlockNum % currentLevel.ClearableNum == 0
//                          && (currentLevel.LevelNum * currentLevel.LevelBlockNum) % (currentLevel.ClearableNum * currentLevel.BlockTypeNum) == 0
//                          && currentLevel.LevelBlockNum <= 16);

//        // Tạo và sắp xếp các khối trên bàn cờ
//        // ...
//        // Đoạn tiếp tục giống logic đã dịch ban đầu
//        // Khởi tạo bàn cờ
//        ChessBox chessBox = InitChessBox(currentLevel.ChessWidthNum, currentLevel.ChessHeightNum);
//        List<BlockType> chessBlocks = new List<BlockType>();

//        // Số lượng khối còn lại để xử lý
//        int remainBlockNum = totalBlockNum - currentLevel.LeftRandomBlocks - currentLevel.RightRandomBlocks;
//        int minWidth = 0;
//        int maxWidth = currentLevel.ChessWidthNum - 2;
//        int minHeight = 0;
//        int maxHeight = currentLevel.ChessHeightNum - 2;

//        // Khởi tạo các khối theo từng lớp
//        for (int i = 0; i < currentLevel.LevelNum; i++)
//        {
//            int blockNum = Mathf.Min(currentLevel.LevelBlockNum, remainBlockNum);

//            // Nếu đây là lớp cuối cùng
//            if (currentLevel.LevelNum - 1 == i)
//                blockNum = remainBlockNum;

//            // Điều chỉnh biên theo logic
//            if (currentLevel.BlockBorderStep > 0 && i > 0)
//            {
//                switch (i % 4)
//                {
//                    case 0: minWidth += currentLevel.BlockBorderStep; break;
//                    case 3: maxWidth -= currentLevel.BlockBorderStep; break;
//                    case 2: minHeight += currentLevel.BlockBorderStep; break;
//                    case 1: maxHeight -= currentLevel.BlockBorderStep; break;
//                }
//            }

//            // Lấy khối từ danh sách
//            var blocks = blockArr.GetRange(pos, blockNum);
//            pos += blockNum;

//            // Thiết lập tọa độ khối
//            HashSet<string> blockPosSet = new HashSet<string>();
//            for (int j = 0; j < blocks.Count; j++)
//            {
//                BlockType block = blocks[j];
//                int nx, ny;
//                string key;

//                if (isRandom)
//                {
//                    // Tạo tọa độ ngẫu nhiên
//                    do
//                    {
//                        nx = Random.Range(minWidth, maxWidth + 1);
//                        ny = Random.Range(minHeight, maxHeight + 1);
//                        key = $"{nx}_{ny}";
//                    } while (blockPosSet.Contains(key));
//                }
//                else
//                {
//                    // Tạo tọa độ sắp xếp đều đặn
//                    int sqrt = Mathf.FloorToInt(Mathf.Sqrt(currentLevel.LevelBlockNum));
//                    nx = (j % sqrt) * 4 + Mathf.FloorToInt((currentLevel.ChessWidthNum - 3 * sqrt) / 2);
//                    if (sqrt % 2 == 0) nx -= 1;
//                    ny = Mathf.FloorToInt(j / sqrt) * 5 + i;
//                    key = $"{nx}_{ny}";
//                }

//                // Lưu tọa độ vào bàn cờ
//                chessBox?.AddBlock(nx, ny, block);
//                blockPosSet.Add(key);

//                // Thiết lập mối quan hệ giữa các khối (gấp)
//                int minX = Mathf.Max(nx - 2, 0);
//                int minY = Mathf.Max(ny - 2, 0);
//                int maxX = Mathf.Min(nx + 3, currentLevel.ChessWidthNum - 2);
//                int maxY = Mathf.Min(ny + 3, currentLevel.ChessHeightNum - 2);

//                int maxLevel = 0;
//                for (int px = minX; px < maxX; px++)
//                {
//                    for (int py = minY; py < maxY; py++)
//                    {
//                        var nearbyBlocks = chessBox?.GetBlocks(px, py);
//                        if (nearbyBlocks != null && nearbyBlocks.Count > 0)
//                        {
//                            var topBlock = nearbyBlocks[nearbyBlocks.Count - 1];
//                            if (topBlock.Id == block.Id) continue;

//                            maxLevel = Mathf.Max(maxLevel, topBlock.Level);
//                            block.LowerIds.Add(topBlock.Id);
//                            topBlock.HigherIds.Add(block.Id);
//                        }
//                    }
//                }

//                block.BoardType = GameBoardEnum.Level;
//                block.X = nx * currentLevel.ChessItemWidth;
//                block.Y = ny * currentLevel.ChessItemHeight;
//                block.Level = maxLevel + 1;

//                // Thêm vào danh sách hiển thị
//                chessBlocks.Add(block);
//            }

//            // Giảm số lượng khối còn lại
//            remainBlockNum -= blockNum;
//            if (remainBlockNum <= 0) break;
//        }

//        // Render các khối bàn cờ
//        foreach (var block in chessBlocks)
//        {
//            GameObject node = Instantiate(blockPrefabs[DataManager.Instance.ThemeIndex], boardLevelNode);
//            node.GetComponent<Block>().Init(block);
//        }

//        // Cập nhật kích thước các node mở rộng
//        float width = currentLevel.SlotNum * currentLevel.ChessItemWidth * 3 + (currentLevel.SlotNum + 1) * 5;
//        boardSlotNode.GetComponent<RectTransform>().sizeDelta = new Vector2(width, boardSlotNode.GetComponent<RectTransform>().sizeDelta.y);
//        boardLevelExtendNode.GetComponent<RectTransform>().sizeDelta = new Vector2(width, boardLevelExtendNode.GetComponent<RectTransform>().sizeDelta.y);

//        // Điều chỉnh vị trí bảng cờ
//        float x = -currentLevel.ChessItemWidth * currentLevel.ChessWidthNum / 2 + currentLevel.ChessWidthNum;
//        float y = currentLevel.ChessItemHeight * currentLevel.ChessHeightNum / 2 - 230;
//        boardLevelNode.localPosition = new Vector3(x, y, 0);

//    }

//    private void ClearNodeHistory()
//    {
//        // Hàm xóa dữ liệu lịch sử
//    }

//    private List<T> Shuffle<T>(List<T> list)
//    {
//        // Hàm trộn ngẫu nhiên danh sách
//        return list;
//    }
//}
