$.ajax({
    url: "231432423",
    method: "GET",
    // data:'body ',
    // dataType:'json',
    // contentType:'json',//dl trả về
    // async:true, //đồng bộ
}.done(function(res) {
    //dosomethign 
}).fail(function(res) {
    // anotherthing 
    //400-badrequest
    //404- sai url
    //500- code server toang :>
    alert('Có lỗi')
}))