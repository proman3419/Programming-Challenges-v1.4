function getRandomArbitrary(min, max, scale) {
  var result = Math.random()*(max - min) + min;
  return Math.floor(result / scale) * scale;
}

function gameOver() {
  background(0);
  fill(255);
  
  var tSize = 50;
  textSize(tSize);
  textAlign(CENTER, CENTER);
  text('Game Over', width/2, height/2);
  tSize = 25;
  textSize(tSize);
  text('Score ' + score, width/2, height/2 + tSize + 15);
  
  noLoop()
}

function initialize() {
  _scale = 10;
  snake = new Snake(_scale);
  fruit = new Fruit(_scale);
  score = 0;
}

var _scale;
var snake;
var fruit;
var score;

function setup() {
  createCanvas(windowWidth, windowHeight);
  initialize();
  frameRate(15);
}

function draw() {
  background(0);
  fruit.show();
  var lost = snake.update();
  snake.show();
  
  if (lost)
    gameOver();

  if (snake.pos.toString() == fruit.pos.toString()) {
    snake.eat();
    score++;
    fruit = new Fruit(_scale);
  }
}

function keyPressed() {
  if ((keyCode === UP_ARROW) && (snake.dir[1] != _scale))
    snake.dir = [0, -_scale];
  if ((keyCode === LEFT_ARROW) && (snake.dir[0] != _scale))
    snake.dir = [-_scale, 0];
  if ((keyCode === DOWN_ARROW) && (snake.dir[1] != -_scale))
    snake.dir = [0, _scale];
  if ((keyCode === RIGHT_ARROW) && (snake.dir[0] != -_scale))
    snake.dir = [_scale, 0];
}