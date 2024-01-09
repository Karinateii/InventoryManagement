var dataTable; 

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $.ajax({
        url: '/admin/LabSupplies/getall',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log("Received JSON data:", data);

            // Flatten the nested LabSupplies array
            const labSupplies = data.data.$values.reduce((acc, item) => {
                acc.push(item);
                if (item.Supplier && item.Supplier.LabSupplies) {
                    item.Supplier.LabSupplies.$values.forEach(supply => {
                        supply.Supplier = { SupplierName: item.Supplier.SupplierName };
                        acc.push(supply);
                    });
                }
                return acc;
            }, []);

            // Use 'labSupplies' as the source for the DataTable
            dataTable = $('#tblData').DataTable({
                "data": labSupplies.filter(item => item.SupplyName),
                "columns": [
                    { data: 'SupplyName', "width": "25%" },
                    { data: 'QuantityOnHand', "width": "15%" },
                    { data: 'ReorderPoint', "width": "15%" },
                    { data: 'Supplier.SupplierName', "width": "25%" },
                    {
                        data: 'SupplyID',
                        "render": function (data) {
                            return `<div class="w-75 btn-group" role="group">
                            <a href="/admin/LabSupplies/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit</a>
                            <a onClick=Delete('/admin/LabSupplies/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i>Delete</a>
                            </div>`

                        },
                        "width": "20%"
                    }
                ]
            });

            console.log("DataTable initialized successfully!");
        },
        error: function (error) {
            console.error("Error fetching data:", error);
        }
    });
}


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    location.reload();
                    toastr.success(data.message);
                }    
            })
        }
   })
}

