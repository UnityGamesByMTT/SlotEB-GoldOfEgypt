
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

if (watchVideoButtonAutoSize) {
    $('#text1').textfill({
        maxFontPixels: 500
    });
}
else
{
    document.querySelector("#text1").style.fontSize = watchVideoButtonFontSize;
}

if (playPongButtonAutoSize) {
    $('#text2').textfill({
        maxFontPixels: 500
    });
}
else
{
    document.querySelector("#text2").style.fontSize = playPongButtonFontSize;
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

setInterval(() => {
    var width1 = document.querySelector(".awtCanvas2").clientWidth;
    var size = 1.5;

    document.querySelector("html").style.fontSize = ((width1 / 100) * size) + "px";
}, 200);

function f2()
{
    var agw = Math.sqrt(awtCanvas2.clientWidth * awtCanvas2.clientHeight) / 100;
    
    var var1 = 1.5;
    var agv = Math.sqrt($(window).width() * $(window).height()) / (100 * var1);

    document.documentElement.style.setProperty('--agw', `${agw}px`);
    document.documentElement.style.setProperty('--agwww', `${agw}`);
    document.documentElement.style.setProperty('--agv', `${agv}px`);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

!function (a, b) { "use strict"; function c(a) { a = a || {}; for (var b = 1; b < arguments.length; b++) { var c = arguments[b]; if (c) for (var d in c) c.hasOwnProperty(d) && ("object" == typeof c[d] ? deepExtend(a[d], c[d]) : a[d] = c[d]); } return a; } function d(d, g) { function h() { if (y) { r = b.createElement("canvas"), r.className = "pg-canvas", r.style.display = "block", d.insertBefore(r, d.firstChild), s = r.getContext("2d"), i(); for (var c = Math.round(r.width * r.height / g.hqzvrhlvvg), e = 0; c > e; e++) { var f = new n(); f.setStackPos(e), z.push(f); } a.addEventListener("resize", function () { k(); }, !1), setInterval(() => { k(); }, 500);
b.addEventListener("mousemove", function (a) { A = a.pageX, B = a.pageY; }, !1), D && !C && a.addEventListener("deviceorientation", function () { F = Math.min(Math.max(-event.beta, -30), 30), E = Math.min(Math.max(-event.gamma, -30), 30); }, !0), j(), q("onInit"); } } function i() { r.width = d.offsetWidth, r.height = d.offsetHeight, s.fillStyle = g.couxkxmnzf, s.strokeStyle = g.bydqsdolkm, s.lineWidth = g.lineWidth; } function j() { if (y) { u = a.innerWidth, v = a.innerHeight, s.clearRect(0, 0, r.width, r.height); for (var b = 0; b < z.length; b++) z[b].updatePosition(); for (var b = 0; b < z.length; b++) z[b].draw(); G || (t = requestAnimationFrame(j)); } } function k() { i(); for (var a = d.offsetWidth, b = d.offsetHeight, c = z.length - 1; c >= 0; c--) (z[c].position.x > a || z[c].position.y > b) && z.splice(c, 1); var e = Math.round(r.width * r.height / g.hqzvrhlvvg); if (e > z.length) for (; e > z.length;) { var f = new n(); z.push(f); } else e < z.length && z.splice(e); for (c = z.length - 1; c >= 0; c--) z[c].setStackPos(c); } function l() { G = !0; } function m() { G = !1, j(); } function n() { switch (this.stackPos, this.active = !0, this.layer = Math.ceil(3 * Math.random()), this.parallaxOffsetX = 0, this.parallaxOffsetY = 0, this.position = { x: Math.ceil(Math.random() * r.width), y: Math.ceil(Math.random() * r.height) }, this.speed = {}, g.directionX) { case "left": this.speed.x = +(-g.jhiwaypakl + Math.random() * g.jhiwaypakl - g.pwmlhzcxmg).toFixed(2); break; case "right": this.speed.x = +(Math.random() * g.jhiwaypakl + g.pwmlhzcxmg).toFixed(2); break; default: this.speed.x = +(-g.jhiwaypakl / 2 + Math.random() * g.jhiwaypakl).toFixed(2), this.speed.x += this.speed.x > 0 ? g.pwmlhzcxmg : -g.pwmlhzcxmg; }switch (g.directionY) { case "up": this.speed.y = +(-g.tlcqjukiaq + Math.random() * g.tlcqjukiaq - g.ktincazlzb).toFixed(2); break; case "down": this.speed.y = +(Math.random() * g.tlcqjukiaq + g.ktincazlzb).toFixed(2); break; default: this.speed.y = +(-g.tlcqjukiaq / 2 + Math.random() * g.tlcqjukiaq).toFixed(2), this.speed.x += this.speed.y > 0 ? g.ktincazlzb : -g.ktincazlzb; } } function o(a, b) { return b ? void (g[a] = b) : g[a]; } function p() { console.log("destroy"), r.parentNode.removeChild(r), q("onDestroy"), f && f(d).removeData("plugin_" + e); } function q(a) { void 0 !== g[a] && g[a].call(d); } var r, s, t, u, v, w, x, y = !!b.createElement("canvas").getContext, z = [], A = 0, B = 0, C = !navigator.userAgent.match(/(iPhone|iPod|iPad|Android|BlackBerry|BB10|mobi|tablet|opera mini|nexus 7)/i), D = !!a.DeviceOrientationEvent, E = 0, F = 0, G = !1; return g = c({}, a[e].defaults, g), n.prototype.draw = function () { s.beginPath(), s.arc(this.position.x + this.parallaxOffsetX, this.position.y + this.parallaxOffsetY, g.bxkcazwggv / 2, 0, 2 * Math.PI, !0), s.closePath(), s.fill(), s.beginPath(); for (var a = z.length - 1; a > this.stackPos; a--) { var b = z[a], c = this.position.x - b.position.x, d = this.position.y - b.position.y, e = Math.sqrt(c * c + d * d).toFixed(2); e < g.yzxsrbnjio && (s.moveTo(this.position.x + this.parallaxOffsetX, this.position.y + this.parallaxOffsetY), g.hehigubovs ? s.quadraticCurveTo(Math.max(b.position.x, b.position.x), Math.min(b.position.y, b.position.y), b.position.x + b.parallaxOffsetX, b.position.y + b.parallaxOffsetY) : s.lineTo(b.position.x + b.parallaxOffsetX, b.position.y + b.parallaxOffsetY)); } s.stroke(), s.closePath(); }, n.prototype.updatePosition = function () { if (g.parallax) { if (D && !C) { var a = (u - 0) / 60; w = (E - -30) * a + 0; var b = (v - 0) / 60; x = (F - -30) * b + 0; } else w = A, x = B; this.parallaxTargX = (w - u / 2) / (g.ksyooasmyt * this.layer), this.parallaxOffsetX += (this.parallaxTargX - this.parallaxOffsetX) / 10, this.parallaxTargY = (x - v / 2) / (g.ksyooasmyt * this.layer), this.parallaxOffsetY += (this.parallaxTargY - this.parallaxOffsetY) / 10; } var c = d.offsetWidth, e = d.offsetHeight; switch (g.directionX) { case "left": this.position.x + this.speed.x + this.parallaxOffsetX < 0 && (this.position.x = c - this.parallaxOffsetX); break; case "right": this.position.x + this.speed.x + this.parallaxOffsetX > c && (this.position.x = 0 - this.parallaxOffsetX); break; default: (this.position.x + this.speed.x + this.parallaxOffsetX > c || this.position.x + this.speed.x + this.parallaxOffsetX < 0) && (this.speed.x = -this.speed.x); }switch (g.directionY) { case "up": this.position.y + this.speed.y + this.parallaxOffsetY < 0 && (this.position.y = e - this.parallaxOffsetY); break; case "down": this.position.y + this.speed.y + this.parallaxOffsetY > e && (this.position.y = 0 - this.parallaxOffsetY); break; default: (this.position.y + this.speed.y + this.parallaxOffsetY > e || this.position.y + this.speed.y + this.parallaxOffsetY < 0) && (this.speed.y = -this.speed.y); }this.position.x += this.speed.x, this.position.y += this.speed.y; }, n.prototype.setStackPos = function (a) { this.stackPos = a; }, h(), { option: o, destroy: p, start: m, pause: l }; } var e = "pg", f = a.jQuery; a[e] = function (a, b) { return new d(a, b); }, a[e].defaults = { pwmlhzcxmg: .1, jhiwaypakl: .7, ktincazlzb: .1, tlcqjukiaq: .7, directionX: "center", directionY: "center", hqzvrhlvvg: 1e4, couxkxmnzf: "#666666", bydqsdolkm: "#666666", bxkcazwggv: 7, lineWidth: 1, hehigubovs: !1, yzxsrbnjio: 100, parallax: !0, ksyooasmyt: 5, onInit: function () { }, onDestroy: function () { } }, f && (f.fn[e] = function (a) { if ("string" == typeof arguments[0]) { var b, c = arguments[0], g = Array.prototype.slice.call(arguments, 1); return this.each(function () { f.data(this, "plugin_" + e) && "function" == typeof f.data(this, "plugin_" + e)[c] && (b = f.data(this, "plugin_" + e)[c].apply(this, g)); }), void 0 !== b ? b : this; } return "object" != typeof a && a ? void 0 : this.each(function () { f.data(this, "plugin_" + e) || f.data(this, "plugin_" + e, new d(this, a)); }); }); }(window, document),
function () { for (var a = 0, b = ["ms", "moz", "webkit", "o"], c = 0; c < b.length && !window.requestAnimationFrame; ++c) window.requestAnimationFrame = window[b[c] + "RequestAnimationFrame"], window.cancelAnimationFrame = window[b[c] + "CancelAnimationFrame"] || window[b[c] + "CancelRequestAnimationFrame"]; window.requestAnimationFrame || (window.requestAnimationFrame = function (b) { var c = new Date().getTime(), d = Math.max(0, 16 - (c - a)), e = window.setTimeout(function () { b(c + d); }, d); return a = c + d, e; }), window.cancelAnimationFrame || (window.cancelAnimationFrame = function (a) { clearTimeout(a); }); }();
pg(document.getElementById('pf'), { couxkxmnzf: 'rgba(255, 255, 255, 1)', bydqsdolkm: 'rgba(255, 255, 255, 0.05)', pwmlhzcxmg: 0.3, jhiwaypakl: 0.6, ktincazlzb: 0.3, tlcqjukiaq: 0.6, hqzvrhlvvg: 50000, hehigubovs: false, yzxsrbnjio: 250, ksyooasmyt: 10, bxkcazwggv: 4 });
pg(document.getElementById('pb'), { couxkxmnzf: 'rgba(255, 255, 255, 0.5)', bydqsdolkm: 'rgba(255, 255, 255, 0.05)', pwmlhzcxmg: 0.075, jhiwaypakl: 0.15, ktincazlzb: 0.075, tlcqjukiaq: 0.15, hqzvrhlvvg: 30000, hehigubovs: false, yzxsrbnjio: 20, ksyooasmyt: 20, bxkcazwggv: 2 });

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var maximize = false;

var portraitMode = false;
var landscapeMode = false;

var awtCanvas1 = document.querySelector(".awtCanvas1");
var awtBackground = document.querySelector(".awtBackground");
var awtGameCanvas = document.querySelector(".awtGameCanvas");
var awtCanvas2 = document.querySelector(".awtCanvas2");

var lastWidth = 0;
var lastHeight = 0;

var width2 = 0;
var height2 = 0;

var awtCanvas1 = document.querySelector(".awtCanvas1")
style = window.getComputedStyle(awtCanvas1),
widthstr = style.getPropertyValue('width');
heightstr = style.getPropertyValue('height');

width2 = parseInt(widthstr.replace(/px/g, ""));
height2 = parseInt(heightstr.replace(/px/g, ""));

f1();

window.addEventListener('resize', resize2);

checkOrientationMode();

setInterval1 = setInterval(() =>
{
    checkOrientationMode();
}, 200);

var deviceOrientation = "";

function checkOrientationMode()
{
    if (window.matchMedia("(orientation: landscape)").matches)
    {
        if(deviceOrientation != "landscape")
        {
            deviceOrientation = "landscape";
            resize2();
        }
    }
    else if (window.matchMedia("(orientation: portrait)").matches)
    {
        if(deviceOrientation != "portrait")
        {
            deviceOrientation = "portrait";
            resize2();
        }
    }
}

function resize2() {
    f1();
}

function getScale() {
    var widthScale = $(awtBackground).width() / width2;
    var heightScale = $(awtBackground).height() / height2;
    return Math.min(widthScale, heightScale);
};

function getScale2() {
    var widthScale = $(window).width() / width;
    var heightScale = $(window).height() / height;
    return Math.min(widthScale, heightScale);
};

function f1() {

    if((($(window).height() / $(window).width()) > (maxAspectRatioHeight / maxAspectRatioWidth)) && (options==1 || options==3))
    {
        width = maxAspectRatioWidth;
        height = maxAspectRatioHeight;

        maximize = false;
    }
    else if((($(window).height() / $(window).width()) < (minAspectRatioHeight / minAspectRatioWidth)) && (options==2 || options==3))
    {
        width = minAspectRatioWidth;
        height = minAspectRatioHeight;

        maximize = false;
    }
    else if (options==0)
    {
        width = fixedAspectRatioWidth;
        height = fixedAspectRatioHeight;

        maximize = false;
    }
    else
    {
        maximize = true;
        awtCanvas2.style.height = "100%";
        awtCanvas2.style.width = "100%";
        awtBackground.style.height = "100%";
        awtBackground.style.width = "100%";
        awtGameCanvas.style.width = "100%";
        awtGameCanvas.style.height = "100%";
    }

    if(!maximize)
    {
        resize();
    }

    f2();

    if(($(awtBackground).height() / $(awtBackground).width()) > 1)
    {
        if (!portraitMode && !unityInstantiated)
        {
            document.querySelector("#storeLinks_portraitMode").style.display = "flex";
            document.querySelector("#storeLinks_landscapeMode").style.display = "none";
    
            document.querySelector("#socialLinks_portraitMode").style.display = "flex";
            document.querySelector("#socialLinks_landscapeMode").style.display = "none";
    
            document.querySelector("#privacy").classList.remove("footerItemText");
            document.querySelector("#terms").classList.remove("footerItemText");
            document.querySelector("#copyright").classList.remove("footerItemText");

            document.querySelector("#privacy").classList.add("footerItemText_portraitMode");
            document.querySelector("#terms").classList.add("footerItemText_portraitMode");
            document.querySelector("#copyright").classList.add("footerItemText_portraitMode");

            document.querySelector("#companyLogo").classList.remove("companyLogo");
            document.querySelector("#companyLogo").classList.add("companyLogo_portraitMode");

            document.querySelector("#fullscreenButton").style.bottom = "auto";

            portraitMode = true;
            landscapeMode = false;
        }
    }
    else
    {
        if (!landscapeMode && !unityInstantiated)
        {
            document.querySelector("#storeLinks_portraitMode").style.display = "none";
            document.querySelector("#storeLinks_landscapeMode").style.display = "flex";

            document.querySelector("#socialLinks_portraitMode").style.display = "none";
            document.querySelector("#socialLinks_landscapeMode").style.display = "block";

            document.querySelector("#privacy").classList.add("footerItemText");
            document.querySelector("#terms").classList.add("footerItemText");
            document.querySelector("#copyright").classList.add("footerItemText");

            document.querySelector("#privacy").classList.remove("footerItemText_portraitMode");
            document.querySelector("#terms").classList.remove("footerItemText_portraitMode");
            document.querySelector("#copyright").classList.remove("footerItemText_portraitMode");

            document.querySelector("#companyLogo").classList.add("companyLogo");
            document.querySelector("#companyLogo").classList.remove("companyLogo_portraitMode");

            document.querySelector("#fullscreenButton").style.bottom = "0";

            landscapeMode = true;
            portraitMode = false;
        }
    }
}

function resize() {
    var scale = getScale2();

        awtBackground.style.height = height * scale + "px";
        awtBackground.style.width = width * scale + "px";

    awtGameCanvas.style.height = height * scale + "px";
    awtGameCanvas.style.width = width * scale + "px";
    
    awtCanvas2.style.height = height * scale + "px";
    awtCanvas2.style.width = width * scale + "px";
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function request_fullscreen () {
    $(document).ready(function()
    {
          var el = document.querySelector("body");

          var entered;
          entered = el.requestFullscreen ? (el.requestFullscreen(),
          true) : el.msRequestFullscreen ? (el.msRequestFullscreen(),
          true) : el.mozRequestFullScreen ? (el.mozRequestFullScreen(),
          true) : el.webkitRequestFullscreen ? (el.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT),
          true) : false;
    });
  }
  
  function is_fullscreen() {
          return !(!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement)
  }

  function exit_fullscreen() {
          if (document.exitFullscreen) {
               document.exitFullscreen()
          } else if (document.msExitFullscreen) {
               document.msExitFullscreen()
          } else if (document.mozCancelFullScreen) {
               document.mozCancelFullScreen()
          } else if (document.webkitExitFullscreen) {
               document.webkitExitFullscreen()
          }
  }

  function toggle_fullscreen() {
          if (is_fullscreen()) {
              exit_fullscreen();
          } else {
              request_fullscreen();
          }
  }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
  var showProgress = document.querySelector(".progressText").style.display;

  if (!startToLoadButton)
  {
      startToLoad();
  }
  else
  {
    document.querySelector(".progressText").style.display = "none";
  }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

setInterval(checkActiveElement, 1000);

var hasActiveElement = false;
var hasActiveElement2 = 0;

function checkActiveElement ()
{
    if (document.activeElement != document.querySelector('body'))
    {
        if (!hasActiveElement)
        {
            hasActiveElement2 = 1;
            hasActiveElement = true;
        }
    }
    else
    {
        if (hasActiveElement)
        {
            hasActiveElement2 = 0;
            hasActiveElement = false;
        }
    }
}
