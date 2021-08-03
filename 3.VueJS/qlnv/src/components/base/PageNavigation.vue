<template>
  <div>
    <div class="page-navigation" :class="{ expanded: contentExpanded }">
      <div class="number-total-info">
        <p>Hiển thị</p>
        <div class="number-total">1-10/1000</div>
        <p>nhân viên</p>
      </div>
      <div class="navigation-button">
        <Button subclass="first-page" />
        <Button subclass="previous-page" />
        <Button subclass="page-number selected" buttonText="1" />
        <Button subclass="page-number" buttonText="2" />
        <Button subclass="page-number" buttonText="3" />
        <Button subclass="next-page" />
        <Button subclass="last-page" />
      </div>
      <div class="per-page-adjust">
        <div class="count">10</div>
        <div>nhân viên/trang</div>
        <div class="adjust-zone">
          <div class="btn increase icon-up"></div>
          <div class="btn decrease icon-down"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "../base/BaseButton.vue";
export default {
  name: "PageNavigation",
  props: {
    contentExpanded: Boolean,
  },
  components: {
    Button,
  },
};
</script>