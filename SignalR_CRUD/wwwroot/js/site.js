//$(() => {
//    LoadProData();
//    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
//    connection.start();
//    connection.on("LoadProducts", function () {
//        LoadProData();
//    })
//    LoadProData();
//    function LoadProData() {
//        var tr = '';
//        $.ajax({
//            url: '/Product/GetProducts',
//            method: 'GET',
//            success: (result) => {
//                $.each(result, (k, v) => {
//                    tr += `<tr>
//                        <td>${v.Name}</td>
//                        <td>${v.Category}</td>
//                        <td>
//                            <a href='../Product/Edit?id=${v.ProdId}'>Edit</a>
//                            <a href='../Product/Details?id=${v.ProdId}'>Details</a>
//                            <a href='../Product/Delete?id=${v.ProdId}'>Delete</a>
//                        </td>
//                    </tr>`
//                })
//                $("#tableBody").html(tr);
//            },
//            errr: (error) => {
//                console.log(error)
//            }
//        });
//    }
//})