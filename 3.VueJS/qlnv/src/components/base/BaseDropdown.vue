<template>
  <div>
    <div>
      <label v-if="labelText">{{ labelText }}</label>
    </div>
    <!--
, closed ? 'isshow' : ''
      $refs.currentDropdown.blur()
     -->
    <button
      :id="id"
      :class="['dropdown', subclass]"
      :myurl="myurl"
      :itemid="itemId"
      :itemname="itemName"
      ref="currentDropdown"
      @click="[ closed ? $refs.currentDropdown.blur(): '']"
      @blur="closeItems()"
    >
      <div @click="toggleItems" class="choice" :value="currentId">
        <div class="divtext">{{ currentName }}</div>
        <div class="arrow-zone">
          <div :class="['arrow', closed ? 'rot-180' : 'rot-0']"></div>
        </div>
      </div>
      <div
        :class="['clearchoice', { 'v-hidden': currentId == '-1' }]"
        @click="callClearChoice"
      ></div>
      <div :class="['itemwrapper']">
        <div
          :class="['item', item[itemId] == currentId ? 'selected' : '']"
          v-for="item in itemlist"
          :key="item[itemId]"
          @click="
            [
              clickItem(item[itemId], item[itemName]),
              $refs.currentDropdown.blur(),
            ]
          "
        >
          {{ item[itemName] }}
        </div>
      </div>
    </button>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "BaseDropdown",
  props: {
    labelText: String,
    id: String,
    subclass: String,
    itemId: String,
    itemName: String,
    myurl: String,
    defaultName: String,
    selectedId: String,
  },
  data() {
    return {
      itemlist: [],
      currentId: "-1",
      currentName: "",
      closed: true,
      defaultId: "-1",
    };
  },
  methods: {
    /**
     * Kiểm tra class của dropdown
     * Gọi API lấy dữ liệu dropdown
     * Hiển thị dữ liệu
     * Nguyễn Hùng 30/07
     */
    loadDropdownData() {
      switch (this.itemName) {
        case "GenderName":
          this.itemlist = [
            {
              Gender: 0,
              GenderName: "Nữ",
            },
            {
              Gender: 1,
              GenderName: "Nam",
            },
            {
              Gender: 2,
              GenderName: "Khác",
            },
          ];
          break;
        case "WorkStatusName":
          this.itemlist = [
            {
              WorkStatus: 0,
              WorkStatusName: "Chờ phỏng vấn",
            },
            {
              WorkStatus: 1,
              WorkStatusName: "Thử việc",
            },
            {
              WorkStatus: 2,
              WorkStatusName: "Đang làm việc",
            },
            {
              WorkStatus: 3,
              WorkStatusName: "Đã nghỉ việc",
            },
          ];
          break;
        case "StoreName":
          this.itemlist = [
            {
              StoreId: 0,
              StoreName: "Nhà hàng Biển Đông",
            },
            {
              StoreId: 1,
              StoreName: "Nhà hàng Biển Tây",
            },
            {
              StoreId: 2,
              StoreName: "Nhà hàng Biển Nam",
            },
            {
              DropdownDetail: 3,
              StoreName: "Nhà hàng Biển Bắc",
            },
          ];
          break;
        case "PositionName":
          this.itemlist = [
            {
              PositionId: 0,
              PositionName: "Nhà hàng Biển Đông",
            },
            {
              PositionId: 1,
              PositionName: "Nhà hàng Biển Tây",
            },
            {
              PositionId: 2,
              PositionName: "Nhà hàng Biển Nam",
            },
            {
              PositionId: 3,
              PositionName: "Nhà hàng Biển Bắc",
            },
          ];
          break;
        case "DepartmentName":
          this.itemlist = [
            {
              DepartmentId: 0,
              DepartmentName: "Nhà hàng Biển Đông",
            },
            {
              DepartmentId: 1,
              DepartmentName: "Nhà hàng Biển Tây",
            },
            {
              DepartmentId: 2,
              DepartmentName: "Nhà hàng Biển Nam",
            },
            {
              DepartmentId: 3,
              DepartmentName: "Nhà hàng Biển Bắc",
            },
          ];
          break;

        default:
          if (this.myurl) {
            axios
              .get(`http://cukcuk.manhnv.net/${this.myurl}`)
              .then((response) => {
                // console.log(response.data);
                this.itemlist = response.data;
              })
              .catch((error) => {
                console.log(error);
              });
          }
          break;
      }
    },

    /**
     * Sự kiện bấm vào text / arrow của dropdown,
     * Hiển thị danh sách lựa chọn
     * Nguyễn Hùng 30/07
     */
    toggleItems() {
      this.closed = !this.closed;
    },
    closeItems() {
      this.closed = true;
    },
    openItems() {
      this.closed = false;
    },
    /**
     * Sự kiện bấm vào 1 lựa chọn
     * Emit sự kiện chứa giá trị và id của lựa chọn vừa bấm cho components cha
     * Nguyễn Hùng 30/07
     */
    clickItem(itemValue, itemName) {
      this.currentId = itemValue;
      this.currentName = itemName;
      this.closed = true;
      this.$emit("input", itemValue);
      // console.log("emitting", itemValue, itemName);
    },
    blurDropdown($event) {
      console.log("blur", $event);
      $event.blur();
    },
    callClearChoice() {
      this.currentId = this.defaultId;
      this.currentName = this.defaultName;
      this.closed = true;
      // this.currentId = -1;
      // this.currentName = "   ";
      this.$emit("input", null, this.itemId);
    },

    /**
     * Khởi tạo các giá trị cho dropdown
     * Nếu là thêm mới thì làm trống các ô
     * Nếu là xem thông tin thì hiển thị thông tin hiện tại
     */
    initChoice() {
      let vm = this;
      if ((vm.selectedId + "").length > 0) {
        vm.itemlist.forEach((item) => {
          if (vm.selectedId == item[vm.itemId]) {
            vm.currentName = item[vm.itemName];
            vm.currentId = item[vm.itemId];
          }
        });
      } else {
        vm.currentId = "-1";
        vm.currentName = "  ";
      }
    },
  },
  created() {
    this.loadDropdownData();
    this.currentName = this.defaultName;
    this.initChoice();
  },
  watch: {
    selectedId: function () {
      this.initChoice();
    },
  },
};
</script>
<style scoped>
@import "../../css/base/dropdown.css";
</style>