<template>
  <div>
    <div
      id="gridEmployee"
      myurl="v1/Employees"
      class="table-wrapper"
      itemid="EmployeeId"
      :class="{'expanded':contentExpanded}"
    >
      <table>
        <thead>
          <tr>
            <th class="align-left" fieldname="Checkbox">
              <div
                class="checkbox"
                @click="selectAllTR"
                :class="{ 'icon-checked': allSelected }"
              ></div>
            </th>
            <th class="align-left" fieldname="No">#</th>
            <th class="align-left" fieldname="EmployeeCode">Mã nhân viên</th>
            <th class="align-left" fieldname="FullName">Họ và tên</th>
            <th class="align-left" fieldname="GenderName">Giới tính</th>
            <th class="align-center" fieldname="DateOfBirth" datatype="Date">
              Ngày sinh
            </th>
            <th class="align-left" fieldname="PhoneNumber">Điện thoại</th>
            <th class="align-left" fieldname="Email">Email</th>
            <th class="align-left" fieldname="PositionName">Chức vụ</th>
            <th class="align-left" fieldname="DepartmentName">Phòng ban</th>
            <th class="align-right" fieldname="Salary" datatype="Number">
              Mức lương cơ bản
            </th>
            <th class="align-left" fieldname="WorkStatus">
              Tình trạng công việc
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(entity, index) in entities"
            :key="entity[itemId]"
            :trid="entity[itemId]"
            :class="[isSelected[index] ? 'checked' : '']"
            @dblclick="dbClickOnTR(entity[itemId])"
          >
            <td class="align-left">
              <div
                @click="selectThisTR(index)"
                :class="{ checkbox: true, 'icon-checked': isSelected[index] }"
              ></div>
            </td>
            <td class="align-left">{{ index + 1 }}</td>
            <td class="align-left">{{ entity.EmployeeCode }}</td>
            <td class="align-left">{{ entity.FullName }}</td>
            <td class="align-left">{{ entity.GenderName }}</td>
            <td class="align-center">
              {{ formatDateDMY(entity.DateOfBirth) }}
            </td>
            <td class="align-left">{{ entity.PhoneNumber }}</td>
            <td class="align-left">{{ entity.Email }}</td>
            <td class="align-left">{{ entity.PositionName }}</td>
            <td class="align-left">{{ entity.DepartmentName }}</td>
            <td class="align-right">{{ formatMoney(entity.Salary) }}</td>
            <td class="align-left">{{ entity.WorkStatus }}</td>
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
    contentExpanded: Boolean
  },
  components: {},
  data() {
    return {
      entities: [],
      isSelected: [],

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
    selectThisTR(index) {
      this.$set(this.isSelected, index, !this.isSelected[index]);
      this.$emit("checkOnItem", this.countTotalSelected());
    },

    countTotalSelected() {
      let selectedCount = 0;
      this.isSelected.forEach((selected) => {
        if (selected) {
          selectedCount += 1;
        }
      });
      console.log(selectedCount);
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
      vm.isSelected.forEach((value, index) => {
        if (value) {
          axios
            .delete(
              `http://cukcuk.manhnv.net/v1/Employees/${
                vm.entities[index][vm.itemId]
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