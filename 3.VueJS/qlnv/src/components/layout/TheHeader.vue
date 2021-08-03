<template>
  <div>
    <div class="header">
      <div class="header-left">
        <div class="toggle"></div>
        <div class="logo"></div>
      </div>
      <div class="header-right">
        <div class="store-selector">
          <div class="store-logo icon-store">BĐ</div>
          <Dropdown
            id="store"
            subclass="store"
            myurl=""
            itemId="StoreId"
            itemName="StoreName"
            selectedId="0"
            defaultName="Nhà hàng biển Đông"
          />
          <!-- <div class="dropdown store">
            <div class="choice">
              <div class="divtext">Nhà hàng Biển Đông</div>
              <div class="arrow-zone">
                <div class="arrow"></div>
              </div>
            </div>
            <div class="itemwrapper">
              <div class="item selected">Nhà hàng Biển Đông</div>
              <div class="item">Nhà hàng Biển Tây</div>
              <div class="item">Nhà hàng Biển Nam</div>
            </div>
          </div> -->
        </div>
        <div class="user">
          <div class="user-notification icon-notification"></div>
          <div class="reddot"></div>
          <div class="user-avatar"></div>
          <div class="user-fullname">Heiiiii Hùng nguyễn</div>
          <div class="user-function"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Dropdown from "../base/BaseDropdown.vue";
export default {
  name: "TheHeader",
  components:{
    Dropdown,
  }
};
</script>
<style>
@import "../../css/layout/header.css";
</style>