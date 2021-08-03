<template>
  <div>
    <div
      id="gridEmployee"
      myurl="v1/Employees"
      class="table-wrapper"
      itemid="EmployeeId"
    >
      <table>
        <thead>
          <tr>
            <th
              v-for="(thItem, thIndex) in thList"
              :key="thIndex"
              :class="thItem.thClass"
            >
              <div
                class="checkbox"
                @click="selectAllTR"
                :class="{ 'icon-checked': allSelected }"
                v-if="thItem.thClass == 'checkboxdiv'"
              ></div>
              {{ thItem.fieldText }}
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(entity, entityIndex) in entities"
            :key="entity[itemId]"
            :class="[isSelected[entityIndex] ? 'checked' : '']"
            @dblclick="dbClickOnTR(entity[itemId])"
          >
            <td
              v-for="(thItem, thIndex) in thList"
              :key="thIndex"
              :class="thItem.thClass"
            >
              <div
                @click="selectThisTR(entityIndex)"
                :class="{ checkbox: true, 'icon-checked': isSelected[entityIndex] }"
                v-if="thItem.thClass == 'checkboxdiv'"
              ></div>
              <div v-else-if="thItem.fieldName == 'No'">{{entityIndex+1}}</div>
              <div v-else>{{ entity[thItem.fieldName] }}</div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { CommonFn } from "../../js/mixins.js";
export default {
  mixins: [CommonFn],
  name: "BaseTable",
  props: {
    warningResponse: String,
  },
  components: {},
  data() {
    return {
      entities: [
        {
          EmployeeCode: "NV-001",
          FullName: "Nguyễn Huy Hùng",
          GenderName: "Nam",
          DateOfBirth: "1999/10/17",
          PhoneNumber: "0123456789",
          Email: "hung@gmail.com",
          PositionName: "Nhân viên",
          DepartmentName: "Phòng Marketting",
          Salary: "10000000",
          WorkStatus: "1",
        },
        {
          EmployeeCode: "NV-001",
          FullName: "Nguyễn Huy Hùng",
          GenderName: "Nam",
          DateOfBirth: "1999/10/17",
          PhoneNumber: "0123456789",
          Email: "hung@gmail.com",
          PositionName: "Nhân viên",
          DepartmentName: "Phòng Marketting",
          Salary: "10000000",
          WorkStatus: "1",
        },
        {
          EmployeeCode: "NV-001",
          FullName: "Nguyễn Huy Hùng",
          GenderName: "Nam",
          DateOfBirth: "1999/10/17",
          PhoneNumber: "0123456789",
          Email: "hung@gmail.com",
          PositionName: "Nhân viên",
          DepartmentName: "Phòng Marketting",
          Salary: "10000000",
          WorkStatus: "1",
        },
        {
          EmployeeCode: "NV-001",
          FullName: "Nguyễn Huy Hùng",
          GenderName: "Nam",
          DateOfBirth: "1999/10/17",
          PhoneNumber: "0123456789",
          Email: "hung@gmail.com",
          PositionName: "Nhân viên",
          DepartmentName: "Phòng Marketting",
          Salary: "10000000",
          WorkStatus: "1",
        },
      ],
      isSelected: [],
      thList: [
        {
          fieldName: "No",
          dataType: "Checkbox",
          fieldText: "",
          thClass: "checkboxdiv",
        },
        {
          fieldName: "No",
          dataType: "Number",
          fieldText: "#",
          thClass: "align-center",
        },

        {              
          fieldName: "EmployeeCode",   //Tên của thuộc tính chứa dữ liệu 
          dataType: "",                //Kiểu dữ liệu checkbox / Number / mặc định là text (tự định nghĩa)
          fieldText: "Mã nhân viên",  // Tên của TH nếu hiện ra 
          thClass: "align-left",     // class tính toán dựa trên dataType, dùng cho css
                                    // text : left,  Number: right,   Date: center
        },
        {
          fieldName: "FullName",
          dataType: "",
          fieldText: "Tên nhân viên",
          thClass: "align-left",
        },
        {
          fieldName: "GenderName",
          dataType: "",
          fieldText: "Giới tính",
          thClass: "align-left",
        },
        {
          fieldName: "DateOfBirth",
          dataType: "",
          fieldText: "Ngày sinh",
          thClass: "align-center",
        },
        {
          fieldName: "PhoneNumber",
          dataType: "",
          fieldText: "Điện thoại",
          thClass: "align-left",
        },
        {
          fieldName: "Email",
          dataType: "",
          fieldText: "Email",
          thClass: "align-left",
        },
        {
          fieldName: "PositionName",
          dataType: "",
          fieldText: "Chức vụ",
          thClass: "align-left",
        },
        {
          fieldName: "DepartmentName",
          dataType: "",
          fieldText: "Phòng ban",
          thClass: "align-left",
        },
        {
          fieldName: "Salary",
          dataType: "",
          fieldText: "Mức lương cơ bản",
          thClass: "align-right",
        },
        {
          fieldName: "WorkStatus",
          dataType: "",
          fieldText: "Tình trạng công việc",
          thClass: "align-left",
        },
      ],
      allSelected: false,
      myurl: "v1/Employees",
      itemId: "EmployeeId",
      //   fieldName =[{aaa},{aaa},{aaa},{aaa},{aaa},{aaa},{aaa},{aaa},{aaa},{aaa},{aaa}]
    };
  },
  methods: {
    /**
     * Gọi API lấy danh sách nhân viên , hiển thị dữ liệu
     * Nguyễn Hùng 30/07
     */
    loadTableData() {
      let vm = this;
      axios
        .get("http://cukcuk.manhnv.net/v1/Employees")
        .then((response) => {
          let resData = response.data.slice(0, 50),
            isSelected = [];

          vm.entities = [];

          resData.forEach(() => {
            isSelected.push(false);
          });
          vm.entities = resData;
          vm.isSelected = isSelected;

          console.log(response.data);
          let toastMessage = {};
          toastMessage.toastType = "Notify";
          toastMessage.toastText = `Cập nhật dữ liệu thành công`;
          this.$emit("showToastMessage", toastMessage);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Khi click vào ô checkbox thứ i
     * Đổi trạng thái của phần tử thứ  i trong mảng đánh dấu true: chọn /false: bỏ chọn
     * Nguyễn Hùng 30/07
     */
    selectThisTR(thIndex) {
      this.$set(this.isSelected, thIndex, !this.isSelected[thIndex]);
      this.$emit("checkOnItem", this.countTotalSelected());

      console.log("single", thIndex)
    },

    countTotalSelected() {
      let selectedCount = 0;
      this.isSelected.forEach((selected) => {
        if (selected) {
          selectedCount += 1;
        }
      });
      console.log("count:",selectedCount);
      return selectedCount;
    },
    /**
     * Khi click vào ô checkbox trên TH
     * Đổi trạng thái được chọn của tất cả phần tử  trong mảng đánh dấu true: chọn /false: bỏ chọn
     * Nguyễn Hùng 31/07
     */
    selectAllTR() {
      this.allSelected = !this.allSelected;
      let allSelectedArray = [...this.isSelected];

      allSelectedArray.fill(this.allSelected);
      this.isSelected = allSelectedArray;
      this.$emit("checkOnItem", this.countTotalSelected());
    },

    btnDeleteOnClick() {
      //nếu hàng này dược chọn
      let message = {},
        totalChecked = this.countTotalSelected(),
        action = "Delete";
      message["warningType"] = "Danger";
      message["text-before"] = "Bạn có muốn xóa dữ liệu của ";
      message["text-target"] = totalChecked + " nhân viên";
      message["text-after"] = "trong hệ thống ?";
      message["confirm-button-text"] = "Xóa";
      this.$emit("showWarningPopup", message, action);
    },
    /**
     * Khi doubleclick vào 1 hàng.
     * Nếu hàng đó được check thì hiện popup hỏi xóa
     * Nếu hàng đó không check  thì gọi hàm hiển thị form detail
     * Nguyễn Hùng 30/07
     */
    dbClickOnTR(itemId) {
      //nếu hàng này không được chọn, emit sự kiện để hiển thị form detail của item
      this.$emit("dbClickOnTR", itemId);
    },

    /**
     * Duyệt danh sách những TR được chọn
     * Lấy ID của entity và gọi API xóa dữ liệu tương ứng
     * Nguyễn Hùng 30/07
     */
    deleteConfirmed() {
      let vm = this,
        totalDeleted = 0,
        hasError = false,
        totalChecked = this.countTotalSelected();
      vm.isSelected.forEach((value, thIndex) => {
        if (value) {
          axios
            .delete(
              `http://cukcuk.manhnv.net/v1/Employees/${
                vm.entities[thIndex][vm.itemId]
              }`
            )
            .catch((error) => {
              console.log(error);
              let toastMessage = {};
              toastMessage.toastType = "Danger";
              toastMessage.toastText = `Quá trình xóa không hoàn tất, đã xóa ${totalDeleted} bản ghi`;
              this.$emit("showToastMessage", toastMessage);
              // alert(
              //   `Quá trình xóa không hoàn tất, đã xóa ${totalDeleted} bản ghi`
              // );
              this.$emit("checkOnItem", this.countTotalSelected());
              hasError = true;
              return;
            });
          totalDeleted = totalDeleted + 1;

          if (totalDeleted == totalChecked) {
            setTimeout(() => {
              this.loadTableData();
            }, 2000);
          }
        }
      });
      if (!hasError) {
        // alert(` Hoàn thành  xóa ${totalDeleted} bản ghi`);
        let toastMessage = {};
        toastMessage.toastType = "Notify";
        toastMessage.toastText = ` Hoàn thành  xóa ${totalDeleted} bản ghi`;
        this.$emit("showToastMessage", toastMessage);
        this.$emit("checkOnItem", 0);
        // this.loadTableData();
      }
    },
  },
  watch: {
    /**
     * Kiểm tra thông điệp khi bấm vào popup xóa, nếu đồng ý, thì gọi hàm xóa
     * Nguyễn Hùng 30/07
     */
    warningResponse: function () {
      let response = this.warningResponse.split("_");
      console.log(response);
      if (response[0] == "ConfirmDelete") {
        this.deleteConfirmed();
      }
    },
  },
  mounted() {
    this.loadTableData();
  },
};
</script>