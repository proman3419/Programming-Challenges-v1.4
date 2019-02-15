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
  text('Game Over', boardSize[0]/2, boardSize[1]/2);
  tSize = 25;
  textSize(tSize);
  text('Score ' + score, boardSize[0]/2, boardSize[1]/2 + tSize + 15);
  
  noLoop()
}

var boardSize = [401, 401];
var _scale = 10;
var snake = new Snake(boardSize, _scale);
var fruit = new Fruit(boardSize, _scale);
var score = 0;

function setup() {
  createCanvas(boardSize[0], boardSize[1]);
  frameRate(10);
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
    fruit = new Fruit(boardSize, _scale);
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