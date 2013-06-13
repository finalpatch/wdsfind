open System.Data
open System.Data.OleDb
open System.Runtime.InteropServices

let queryWDS term =
    let conn = new OleDbConnection("Provider=Search.CollatorDSO.1;Extended Propertes=\"Application=Windows\"")
    do conn.Open()
    let q = sprintf "SELECT Top 100 System.ItemPathDisplay FROM SYSTEMINDEX
                     WHERE FREETEXT('%s') OR
                     System.ItemNameDisplay LIKE '%%%s%%'" term term
    let cmd = new OleDbCommand(q, conn)
    cmd.ExecuteReader()
    |> Seq.cast<IDataRecord>
    |> Seq.map (fun o -> o.GetString 0)

[<DllImport("kernel32.dll")>]
extern bool SetConsoleOutputCP(uint32 wCodePageID);

[<EntryPoint>]
let main args =
    SetConsoleOutputCP 65001u |> ignore
    if (Array.length args) >= 1 then
        queryWDS (String.concat " " args)
        |> Seq.map (fun s -> s.Replace("\\", "/"))
        |> Seq.iter (printfn "%s")
    else
        printfn "Usage: wdsfind term"
    0
